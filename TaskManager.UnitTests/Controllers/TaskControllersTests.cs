using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManager.API.Controllers;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interface;
using TaskManager.Core.Entities;
using TaskManager.Core.Enums;

namespace TaskManager.UnitTests.Controllers
{
    public class TaskControllersTests
    {
        private Mock<ITaskService> _taskService;

        public TaskControllersTests()
        {
            _taskService = new Mock<ITaskService>();
        }

        [Fact]
        public async void Create_ShouldReturnCreatedAtActionResult_WhenTaskIsSuccessfullyCreated()
        {

            var responseSuccess = ResultViewModel<TaskEntity>.Success(new TaskEntity());
            var createdTask = new CreateTask()
            {
                Description = "Test",
                Title = " TitleTest",
                ExpectedCompletionDate = DateTime.Now
            };

            var taskController = new TasksController(_taskService.Object);
            _taskService.Setup(m => m.Create(It.IsAny<CreateTask>()))
                      .ReturnsAsync(responseSuccess);

            var result = await taskController.Create(createdTask);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(201, createdResult.StatusCode);
            _taskService.Verify(t => t.Create(It.IsAny<CreateTask>()), Times.Once);
        }

        [Fact]
        public async void Create_ShouldReturn500Status_WhenServiceThrowsException()
        {
            var createdTask = new CreateTask()
            {
                Description = "Test",
                Title = " TitleTest",
                ExpectedCompletionDate = DateTime.Now
            };

            var taskController = new TasksController(_taskService.Object);
            _taskService.Setup(m => m.Create(It.IsAny<CreateTask>()))
                      .ThrowsAsync(new Exception());

            var result = await taskController.Create(createdTask);

            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, createdResult.StatusCode);
            _taskService.Verify(t => t.Create(It.IsAny<CreateTask>()), Times.Once);
        }


        [Fact]
        public async void UpdateStatus_ShouldReturnNoContent_WhenUpdateIsSuccessful()
        {
            var responseSuccess = ResultViewModel<string>.Success("result teste");
            var updateStatusTask = new UpdateStatusTask()
            {
                Status = TaskState.Pending,
            };

            var taskController = new TasksController(_taskService.Object);
            _taskService.Setup(m => m.UpdateStatus(It.IsAny<UpdateStatusTask>()))
                      .ReturnsAsync(responseSuccess);

            var result = await taskController.UpdateStatus(Guid.NewGuid(), updateStatusTask);

            var createdResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, createdResult.StatusCode);
            _taskService.Verify(t => t.UpdateStatus(It.IsAny<UpdateStatusTask>()), Times.Once);
        }

        [Fact]
        public async void UpdateStatus_ShouldReturnNotFound_WhenTaskIsNotFound()
        {

            var responseError = ResultViewModel<string>.Error("result teste");
            var updateStatusTask = new UpdateStatusTask()
            {
                Status = TaskState.Pending,
            };

            var taskController = new TasksController(_taskService.Object);
            _taskService.Setup(m => m.UpdateStatus(It.IsAny<UpdateStatusTask>()))
                      .ReturnsAsync(responseError);

            var result = await taskController.UpdateStatus(Guid.NewGuid(), updateStatusTask);

            var createdResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, createdResult.StatusCode);
            _taskService.Verify(t => t.UpdateStatus(It.IsAny<UpdateStatusTask>()), Times.Once);
        }

        [Fact]
        public async void UpdateStatus_ShouldReturn500_WhenServiceThrowsException()
        {
            var updateStatusTask = new UpdateStatusTask()
            {
                Status = TaskState.Pending,
            };

            var taskController = new TasksController(_taskService.Object);
            _taskService.Setup(m => m.UpdateStatus(It.IsAny<UpdateStatusTask>()))
                      .ThrowsAsync(new Exception());

            var result = await taskController.UpdateStatus(Guid.NewGuid(), updateStatusTask);

            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, createdResult.StatusCode);
            _taskService.Verify(t => t.UpdateStatus(It.IsAny<UpdateStatusTask>()), Times.Once);
        }

        [Fact]
        public async void GetById_ShouldReturn200_WhenTaskIsFound()
        {
            var responseSuccess = ResultViewModel<string>.Success("Success result teste");
            var updateStatusTask = new UpdateStatusTask()
            {
                Status = TaskState.Pending,
            };

            var taskController = new TasksController(_taskService.Object);
            _taskService.Setup(m => m.GetById(It.IsAny<Guid>()))
                      .ReturnsAsync(responseSuccess);

            var result = await taskController.GetById(Guid.NewGuid());

            var createdResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, createdResult.StatusCode);
            _taskService.Verify(t => t.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void GetById_ShouldReturnNotFound_WhenTaskIsNotFound()
        {
            var responseError = ResultViewModel<string>.Error("result teste");
            var updateStatusTask = new UpdateStatusTask()
            {
                Status = TaskState.Pending,
            };

            var taskController = new TasksController(_taskService.Object);
            _taskService.Setup(m => m.GetById(It.IsAny<Guid>()))
                      .ReturnsAsync(responseError);

            var result = await taskController.GetById(Guid.NewGuid());

            var createdResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, createdResult.StatusCode);
            _taskService.Verify(t => t.GetById(It.IsAny<Guid>()), Times.Once);
        }



        [Fact]
        public async void GetById_ShouldReturn500_WhenServiceThrowsException()
        {
            var taskController = new TasksController(_taskService.Object);
            _taskService.Setup(m => m.GetById(It.IsAny<Guid>()))
                      .ThrowsAsync(new Exception());

            var result = await taskController.GetById(Guid.NewGuid());

            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, createdResult.StatusCode);
            _taskService.Verify(t => t.GetById(It.IsAny<Guid>()), Times.Once);
        }


        [Fact]
        public async void GetAll_ShouldReturnOk_WhenServiceReturnsSuccess()
        {
            var responseSuccess = ResultViewModel<List<TaskViewModel>>.Success(new List<TaskViewModel>());
            var updateStatusTask = new UpdateStatusTask()
            {
                Status = TaskState.Pending,
            };

            var taskController = new TasksController(_taskService.Object);
            _taskService.Setup(m => m.GetAll())
                      .Returns(responseSuccess);

            var result = await taskController.GetAll();

            var createdResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, createdResult.StatusCode);
            _taskService.Verify(t => t.GetAll(), Times.Once);
        }


        [Fact]
        public async void GetAll_ShouldReturn500_WhenServiceThrowsException()
        {
            var taskController = new TasksController(_taskService.Object);
            _taskService.Setup(t => t.GetAll())
                      .Throws(new Exception());

            var result = await taskController.GetAll();

            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, createdResult.StatusCode);
            _taskService.Verify(t => t.GetAll(), Times.Once);
        }

        [Fact]
        public async void Delete_ShouldReturnNoContent_WhenUpdateIsSuccessfull()
        {
            var responseSuccess = ResultViewModel<string>.Success("result teste");
            
            var taskController = new TasksController(_taskService.Object);
            _taskService.Setup(m => m.Delete(It.IsAny<Guid>()))
                      .ReturnsAsync(responseSuccess);

            var result = await taskController.Delete(Guid.NewGuid());

            var createdResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, createdResult.StatusCode);
            _taskService.Verify(t => t.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void Delete_ShouldReturnNotFound_WhenTaskIsNotFound()
        {

            var responseError = ResultViewModel<string>.Error("result teste");
         
            var taskController = new TasksController(_taskService.Object);
            _taskService.Setup(m => m.Delete(It.IsAny<Guid>()))
                      .ReturnsAsync(responseError);

            var result = await taskController.Delete(Guid.NewGuid());

            var createdResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, createdResult.StatusCode);
            _taskService.Verify(t => t.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void Delete_ShouldReturn500_WhenServiceThrowsException()
        {
            var taskController = new TasksController(_taskService.Object);
            _taskService.Setup(t => t.Delete(It.IsAny<Guid>()))
                      .Throws(new Exception());

            var result = await taskController.Delete(Guid.NewGuid());

            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, createdResult.StatusCode);
            _taskService.Verify(t => t.Delete(It.IsAny<Guid>()), Times.Once);
        }
    }
}
