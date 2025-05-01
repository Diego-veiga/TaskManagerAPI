
using TaskManager.Core.Enums;

namespace TaskManager.Core.DTOs
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskState Status { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
    }
}
