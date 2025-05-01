
using TaskManager.Application.DTOs;
using TaskManager.Application.Validations;

namespace TaskManager.UnitTests.Validation
{
    public class CreateTaskValidationTask
    {
        [Fact]
        public void CreateTaskValidation_ShouldPass_WhenAllFieldsAreValid()
        {
            var createTask = new CreateTask()
            {
                Title = "Title Test",
                ExpectedCompletionDate = DateTime.Now,
                Description = " Description Test",
            };
            var createTaskValidation = new CreateTaskValidation();

            var result = createTaskValidation.Validate(createTask);

            Assert.True(result.IsValid);
            Assert.Equal(0, result.Errors.Count());
        }

        [Fact]
        public void CreateTaskValidation_ShouldFail_WhenTitleIsEmpty()
        {
            var createTask = new CreateTask()
            {
                ExpectedCompletionDate = DateTime.Now,
                Description = " Description Test",
            };
            var createTaskValidation = new CreateTaskValidation();

            var result = createTaskValidation.Validate(createTask);

            Assert.False(result.IsValid);
            Assert.Equal("'Title' must not be empty.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void CreateTaskValidation_ShouldFail_WhenTitleIsTooShort()
        {
            var createTask = new CreateTask()
            {
                Title = "f",
                ExpectedCompletionDate = DateTime.Now,
                Description = " Description Test",
            };
            var createTaskValidation = new CreateTaskValidation();

            var result = createTaskValidation.Validate(createTask);

            Assert.False(result.IsValid);
            Assert.Equal("The length of 'Title' must be at least 3 characters. You entered 1 characters.", result.Errors[0].ErrorMessage);
        }

        [Fact]
        public void CreateTaskValidation_ShouldFail_WhenExpectedCompletionDateIsInThePast()
        {
            var createTask = new CreateTask()
            {
                Title = "teste025",
                ExpectedCompletionDate = DateTime.Now.AddDays(-2),
                Description = " Description Test",
            };
            var createTaskValidation = new CreateTaskValidation();

            var result = createTaskValidation.Validate(createTask);

            Assert.False(result.IsValid);
            Assert.Equal("The date cannot be later than today.", result.Errors[0].ErrorMessage);
        }



        [Fact]
        public void CreateTaskValidation_ShouldFail_WhenDescriptionIsEmpty()
        {
            var createTask = new CreateTask()
            {
                Title = "teste025",
                ExpectedCompletionDate = DateTime.Now.AddDays(2),
            };
            var createTaskValidation = new CreateTaskValidation();

            var result = createTaskValidation.Validate(createTask);

            Assert.False(result.IsValid);
            Assert.Equal("'Description' must not be empty.", result.Errors[0].ErrorMessage);
        }
    }
}
