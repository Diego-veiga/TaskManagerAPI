using System.Text.Json.Serialization;
using TaskManager.Core.Enums;

namespace TaskManager.Core.DTOs
{
    public class UpdateStatusTask
    {
        [JsonIgnore] 
        public Guid Id { get; set; }
        public TaskState Status  { get; set; }
    }
}
