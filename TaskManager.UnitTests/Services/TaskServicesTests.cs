
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;
using System.Threading;
using TaskManager.Application;
using TaskManager.Application.DTOs;
using TaskManager.Core.Entities;
using TaskManager.Core.Enums;
using TaskManager.Core.Interfaces;

namespace TaskManager.UnitTests.Services
{
    public class TaskServicesTests
    {
        public Mock<IUnitOFWork> _unitOfWorkMock { get; set; }
        public TaskServicesTests()
        {
            _unitOfWorkMock = new Mock<IUnitOFWork>();
        }


        [Fact]
        public async void HCreate_ShouldAddTaskAndCommit_WhenDataIsValid()
        {
            // Arrange
            _unitOfWorkMock.Setup(uow => uow.taskRepository.Add(It.IsAny<TaskEntity>()));
            _unitOfWorkMock.Setup(uow => uow.Commit());

            var createTask = new CreateTask()
            {
                Title = "Test",
                Description = " Description Test",
                ExpectedCompletionDate = DateTime.Now,
            };

            var taskService = new TaskService(_unitOfWorkMock.Object);

            // Act
            await taskService.Create(createTask);

            // Assert
            _unitOfWorkMock.Verify(c => c.taskRepository.Add(It.IsAny<TaskEntity>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Once());
        }

        [Fact]
        public async void Create_ShouldThrowException_WhenRepositoryAddFails()
        {
            // Arrange
            _unitOfWorkMock.Setup(uow => uow.taskRepository.Add(It.IsAny<TaskEntity>())).Throws(new Exception());
            _unitOfWorkMock.Setup(uow => uow.Commit());

            var createTask = new CreateTask()
            {
                Title = "Test",
                Description = " Description Test",
                ExpectedCompletionDate = DateTime.Now,
            };

            var taskService = new TaskService(_unitOfWorkMock.Object);

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                      taskService.Create(createTask));


            // Assert
            _unitOfWorkMock.Verify(c => c.taskRepository.Add(It.IsAny<TaskEntity>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Never);
        }

        [Fact]
        public async void Delete_ShouldSucceed_WhenTaskIsFound()
        {
            // Arrange
            _unitOfWorkMock
                           .Setup(uow => uow.taskRepository.FindByParam(It.IsAny<Expression<Func<TaskEntity, bool>>>())).ReturnsAsync(new TaskEntity());
            _unitOfWorkMock.Setup(uow => uow.taskRepository.Delete(It.IsAny<TaskEntity>()));
            _unitOfWorkMock.Setup(uow => uow.Commit());



            var taskService = new TaskService(_unitOfWorkMock.Object);

            // Act
            var result = await taskService.Delete(Guid.NewGuid());

            // Assert
            Assert.True(result.IsSuccess);
            _unitOfWorkMock.Verify(c => c.taskRepository.FindByParam(It.IsAny<Expression<Func<TaskEntity, bool>>>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.taskRepository.Delete(It.IsAny<TaskEntity>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Once());
        }


        [Fact]
        public async void Delete_ShouldFail_WhenTaskNotFound()
        {
            // Arrange
            _unitOfWorkMock
                           .Setup(uow => uow.taskRepository.FindByParam(It.IsAny<Expression<Func<TaskEntity, bool>>>()));
            _unitOfWorkMock.Setup(uow => uow.taskRepository.Delete(It.IsAny<TaskEntity>()));
            _unitOfWorkMock.Setup(uow => uow.Commit());

            var taskService = new TaskService(_unitOfWorkMock.Object);

            // Act
            var result = await taskService.Delete(Guid.NewGuid());

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Task not Found", result.Message);
            _unitOfWorkMock.Verify(c => c.taskRepository.FindByParam(It.IsAny<Expression<Func<TaskEntity, bool>>>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.taskRepository.Delete(It.IsAny<TaskEntity>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Never());
        }

        [Fact]
        public async void GetById_ShouldReturnSuccess_WhenTaskFound()
        {
            // Arrange
            _unitOfWorkMock
                           .Setup(uow => uow.taskRepository.FindByParam(It.IsAny<Expression<Func<TaskEntity, bool>>>())).ReturnsAsync(new TaskEntity());


            var taskService = new TaskService(_unitOfWorkMock.Object);

            // Act
            var result = await taskService.GetById(Guid.NewGuid());

            // Assert
            Assert.True(result.IsSuccess);
            _unitOfWorkMock.Verify(c => c.taskRepository.FindByParam(It.IsAny<Expression<Func<TaskEntity, bool>>>()), Times.Once());
        }

        [Fact]
        public async void GetById_ShouldFail_WhenTaskNotFound()
        {
            // Arrange
            _unitOfWorkMock
                           .Setup(uow => uow.taskRepository.FindByParam(It.IsAny<Expression<Func<TaskEntity, bool>>>()));


            var taskService = new TaskService(_unitOfWorkMock.Object);

            // Act
            var result = await taskService.GetById(Guid.NewGuid());

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Task not Found", result.Message);
            _unitOfWorkMock.Verify(c => c.taskRepository.FindByParam(It.IsAny<Expression<Func<TaskEntity, bool>>>()), Times.Once());

        }

        [Fact]
        public void GetAll_ShouldReturnTasks_WhenTasksExist()
        {
            // Arrange
            var fakeTasks = new List<TaskEntity>
                {
                    new TaskEntity("Tarefa 1", "Descrição 1", DateTime.Now),
                    new TaskEntity("Tarefa 2", "Descrição 2", DateTime.Now.AddDays(1))
                };

            _unitOfWorkMock
                .Setup(uow => uow.taskRepository.Get())
                .Returns(fakeTasks.AsQueryable());


            var taskService = new TaskService(_unitOfWorkMock.Object);

            // Act
            var result = taskService.GetAll();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Data?.Count());
            _unitOfWorkMock.Verify(c => c.taskRepository.Get(), Times.Once());
        }

        [Fact]
        public async void Delete_ShouldSucceed_WhenTaskIsFound2()
        {
            // Arrange
            _unitOfWorkMock
                           .Setup(uow => uow.taskRepository.FindByParam(It.IsAny<Expression<Func<TaskEntity, bool>>>())).ReturnsAsync(new TaskEntity());
            _unitOfWorkMock.Setup(uow => uow.taskRepository.Update(It.IsAny<TaskEntity>()));
            _unitOfWorkMock.Setup(uow => uow.Commit());

            var updateStatusTask = new UpdateStatusTask
            {
                Id = Guid.NewGuid(),
                Status = TaskState.Concluded
            };

            var taskService = new TaskService(_unitOfWorkMock.Object);

            // Act
            var result = await taskService.UpdateStatus(updateStatusTask);

            // Assert
            Assert.True(result.IsSuccess);
            _unitOfWorkMock.Verify(c => c.taskRepository.FindByParam(It.IsAny<Expression<Func<TaskEntity, bool>>>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.taskRepository.Update(It.IsAny<TaskEntity>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Once());
        }


        [Fact]
        public async void Delete_ShouldFail_WhenTaskNotFound1()
        {
            // Arrange
            _unitOfWorkMock
                           .Setup(uow => uow.taskRepository.FindByParam(It.IsAny<Expression<Func<TaskEntity, bool>>>()));
            _unitOfWorkMock.Setup(uow => uow.taskRepository.Update(It.IsAny<TaskEntity>()));
            _unitOfWorkMock.Setup(uow => uow.Commit());

            var updateStatusTask = new UpdateStatusTask
            {
                Id = Guid.NewGuid(),
                Status = TaskState.Concluded
            };

            var taskService = new TaskService(_unitOfWorkMock.Object);

            // Act
            var result = await taskService.UpdateStatus(updateStatusTask);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Task not Found", result.Message);
            _unitOfWorkMock.Verify(c => c.taskRepository.FindByParam(It.IsAny<Expression<Func<TaskEntity, bool>>>()), Times.Once());
            _unitOfWorkMock.Verify(c => c.taskRepository.Update(It.IsAny<TaskEntity>()), Times.Never());
            _unitOfWorkMock.Verify(c => c.Commit(), Times.Never());
        }
    }
}
