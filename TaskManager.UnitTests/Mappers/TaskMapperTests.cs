using TaskManager.Application.Mappers;
using TaskManager.Core.Entities;

namespace TaskManager.UnitTests.Mappers
{
    public  class TaskMapperTests
    {
        [Fact]

        public void ToViewModel_ShouldMapTaskProperties_Correctly()
        {
            var task = new TaskEntity("Task01", "Description01", DateTime.Now);

            var taskViewModel = TaskMapper.ToViewModel(task);

            Assert.NotNull(taskViewModel);
            Assert.Equal(taskViewModel.Id, taskViewModel.Id);
            Assert.Equal(taskViewModel.Title, taskViewModel.Title);
            Assert.Equal(taskViewModel.Description, taskViewModel.Description);
            Assert.Equal(taskViewModel.Status, taskViewModel.Status);
            Assert.Equal(taskViewModel.Active, taskViewModel.Active);
            Assert.Equal(taskViewModel.ExpectedCompletionDate, taskViewModel.ExpectedCompletionDate);
            Assert.Equal(taskViewModel.CreatedAt, taskViewModel.CreatedAt);
            Assert.Equal(taskViewModel.UpdatedAt, taskViewModel.UpdatedAt);
        }
    }
}
