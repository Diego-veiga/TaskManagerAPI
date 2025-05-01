using TaskManager.Core.Enums;

namespace TaskManager.Core.Entities
{
    public class TaskEntity : BaseEntity
    {
        public TaskEntity(string title, string description, DateTime expectedCompletionDate)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Status = TaskState.Pending;
            this.expectedCompletionDate = expectedCompletionDate;
            CreatedAt = DateTime.UtcNow;
            Active = true;
        }

        public TaskEntity()
        {

        }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskState Status { get; set; }
        public DateTime expectedCompletionDate { get; set; }



    }
}
