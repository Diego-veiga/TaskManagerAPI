using TaskManager.Core.Enums;

namespace TaskManager.Core.DTOs
{
    public class CreateTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskState Status { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
    }
}
