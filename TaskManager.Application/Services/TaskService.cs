using TaskManager.Application.DTOs;
using TaskManager.Application.Interface;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Entities;
using TaskManager.Application.Mappers;

namespace TaskManager.Application
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOFWork _unitOFWork;

        public TaskService(IUnitOFWork unitOFWork)
        {
            _unitOFWork = unitOFWork;
        }

        public async Task<ResultViewModel<TaskEntity>> Create(CreateTask createTask)
        {
            var newTask = new TaskEntity(createTask.Title, createTask.Description, createTask.ExpectedCompletionDate);
            _unitOFWork.taskRepository.Add(newTask);
            await _unitOFWork.Commit();
            return ResultViewModel<TaskEntity>.Success(newTask);
        }

        public async Task<ResultViewModel> Delete(Guid Id)
        {
            var task = await _unitOFWork.taskRepository.FindByParam(t => t.Id == Id && t.Active);
            if (task == null)
            {
                return ResultViewModel<string>.Error("Task not Found");
            }
            _unitOFWork.taskRepository.Delete(task);
            await _unitOFWork.Commit();

            return ResultViewModel.Success();

        }

        public ResultViewModel<List<TaskViewModel>> GetAll()
        {
            var tasks = _unitOFWork.taskRepository.Get().Where(t => t.Active).ToList();
            var tasksViewModel = GetListViewModel(tasks);
            return ResultViewModel<List<TaskViewModel>>.Success(tasksViewModel);
        }

        private List<TaskViewModel> GetListViewModel(List<TaskEntity> tasks)
        {
            var tasksViewModel = new List<TaskViewModel>();
            foreach (var task in tasks)
            {
                var taskViewModel = TaskMapper.ToViewModel(task);
                tasksViewModel.Add(taskViewModel);
            }

            return tasksViewModel;
        }

        public async Task<ResultViewModel> GetById(Guid id)
        {
            var task = await _unitOFWork.taskRepository.FindByParam(t => t.Id == id && t.Active);
            if (task == null)
            {
                return ResultViewModel<string>.Error("Task not Found");
            }

            var taskViewModel = TaskMapper.ToViewModel(task);
            return ResultViewModel<TaskViewModel>.Success(taskViewModel);

        }

        public async Task<ResultViewModel> UpdateStatus(UpdateStatusTask updateStatusTask)
        {
            var task = await _unitOFWork.taskRepository.FindByParam(t => t.Id == updateStatusTask.Id && t.Active);
            if (task == null)
            {
                return ResultViewModel<string>.Error("Task not Found");
            }
            task.Status = updateStatusTask.Status;
            _unitOFWork.taskRepository.Update(task);
            await _unitOFWork.Commit();

            return ResultViewModel<string>.Success("Task successfully updated");
        }


    }
}
