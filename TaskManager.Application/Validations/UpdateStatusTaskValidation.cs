using FluentValidation;
using TaskManager.Application.DTOs;
using TaskManager.Core.Enums;

namespace TaskManager.Application.Validations
{
    public class UpdateStatusTaskValidation : AbstractValidator<UpdateStatusTask>
    {
        public UpdateStatusTaskValidation()
        {
            RuleFor(x => x.Status)
               .Must(valor => Enum.IsDefined(typeof(TaskState), valor))
               .WithMessage("Invalid status. Enter 1 for Pending and 2 for Completed");
        }
    }
}
