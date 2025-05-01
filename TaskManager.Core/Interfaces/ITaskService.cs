
using TaskManager.Core.DTOs;
using TaskManager.Core.Entities;

namespace TaskManager.Core.Interfaces
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
