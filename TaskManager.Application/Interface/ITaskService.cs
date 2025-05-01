using TaskManager.Application.DTOs;
using TaskManager.Core.Entities;

namespace TaskManager.Application.Interface
{
    public interface ITaskService
    {
        Task<ResultViewModel<TaskEntity>> Create(CreateTask createTask);
        ResultViewModel<List<TaskViewModel>> GetAll();
        Task<ResultViewModel> GetById(Guid id);
        Task<ResultViewModel> UpdateStatus(UpdateStatusTask updateStatusTask);
        Task<ResultViewModel> Delete(Guid id);
    }
}
