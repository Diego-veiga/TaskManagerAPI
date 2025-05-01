

using System.Threading.Tasks;
using TaskManager.Core.Entities;
using TaskManager.Core.Enums;

namespace TaskManager.UnitTests.Entities
{
    public class TaskEntityTests
    {
        [Fact]
        public void Constructor_ShouldInitializeTaskEntityWithDefaultValues()
        {
            // Arrange Act

            var entity = new TaskEntity("Task01", "Description01", DateTime.Now);

            // Assert
            Assert.IsType<TaskEntity>(entity);
            Assert.NotNull(entity.Id);
            Assert.NotNull(entity.CreatedAt);
            Assert.Equal(entity.Title, "Task01");
            Assert.Equal(entity.Description, "Description01");
            Assert.Equal(entity.Status, TaskState.Pending);
            Assert.True(entity.Active);
        } 
    }
}
