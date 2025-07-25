using FluentValidation;
using TaskifyAPI.Dtos.TaskItemDtos;

namespace TaskifyAPI.Validators;

public class UpdateTaskItemDtoValidator : AbstractValidator<UpdateTaskItemDto>
{
    public UpdateTaskItemDtoValidator()
    {
        RuleFor(x => x.Status)
            .NotNull()
            .WithMessage("Status cannot be null");
    }
}