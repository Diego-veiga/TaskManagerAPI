using FluentValidation;
using TaskManager.Application.DTOs;
using TaskManager.Application.Validations;


namespace TaskManager.UnitTests.Validation
{
    public class UpdateStatusTaskValidationTests
    {
        [Fact]
        public void CreateTaskValidation_ShouldPass_WhenAllFieldsAreValid1()
        {
            var updateStatusTask = new UpdateStatusTask()
            {
                Id = Guid.NewGuid(),
                Status = Core.Enums.TaskState.Pending
            };
                            
            var updateStatusTaskValidation = new UpdateStatusTaskValidation();

            var result = updateStatusTaskValidation.Validate(updateStatusTask);

            Assert.True(result.IsValid);
            Assert.Equal(0, result.Errors.Count());
        }

        [Fact]
        public void CreateTaskValidation_ShouldFail_WhenTitleIsEmpty2()
        {
           var updateStatusTask = new UpdateStatusTask()
            {
                Id = Guid.NewGuid(),
                Status = 0
            };
                            
            var updateStatusTaskValidation = new UpdateStatusTaskValidation();

            var result = updateStatusTaskValidation.Validate(updateStatusTask);

            Assert.False(result.IsValid);
            Assert.Equal("Invalid status. Enter 1 for Pending and 2 for Completed", result.Errors[0].ErrorMessage);
        }


    }
}
