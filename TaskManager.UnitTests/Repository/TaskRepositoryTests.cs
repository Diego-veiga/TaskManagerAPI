using TaskManager.Core.Entities;
using TaskManager.Infrastructure.Repositories;
using TaskManager.UnitTests.Helper;

namespace TaskManager.UnitTests.Repository
{
    public class TaskRepositoryTests
    {

        [Fact]
        public async void GetByTaskId_ExistingObjects_ReturnTask()
        {
            // Arrange
            var context = new RepositoryTestsHelper().GetInMemoryAppDbContext("TestesBank");
            var taskId = Guid.NewGuid();;
            var expectedCompletionDate = DateTime.Now;
            var task = new TaskEntity("Task01", "Description01", expectedCompletionDate);
            context.Tasks.Add(task);
            context.SaveChanges();
            var bankRepository = new TaskRepository(context);
            
            // Act
            var result = await bankRepository.FindByParam(u => u.Id == task.Id);
                        
            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.Title, "Task01");
            Assert.Equal(result.Description, "Description01");
            Assert.Equal(result.ExpectedCompletionDate, expectedCompletionDate);
        }

        
    }
}
