using TaskManager.Core.Enums;

namespace TaskManager.Application.DTOs
{
    public class CreateTask
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpectedCompletionDate { get; set; }
    }
}
