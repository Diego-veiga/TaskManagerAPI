
using TaskManager.Application.DTOs;
using TaskManager.Core.Entities;

namespace TaskManager.Application.Mappers
{
    public class TaskMapper
    {
        public static TaskViewModel ToViewModel(TaskEntity task)
        {
            return new TaskViewModel()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                ExpectedCompletionDate = task.ExpectedCompletionDate,
                Status = task.Status,
                Active = task.Active,
                CreatedAt = task.CreatedAt,
            };
        }
    }
}
