using FluentValidation;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Validations
{
    public class CreateTaskValidation : AbstractValidator<CreateTask>
    {
        public CreateTaskValidation()
        {
            RuleFor(x => x.Title)
                   .NotEmpty()
                   .NotNull()
                   .MinimumLength(3)
                   .MaximumLength(25)
                   .WithMessage("The task title must be between 3 and 25 digits long");


            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("The task description must not be blank");


            RuleFor(x => x.ExpectedCompletionDate)
                .GreaterThanOrEqualTo(DateTime.Today)
                .WithMessage("The date cannot be later than today.");


        }
    }
}
