using FluentValidation;
using TaskifyAPI.Dtos.TaskItemDtos;

namespace TaskifyAPI.Validators;

public class CreateTaskItemDtoValidator : AbstractValidator<CreateTaskItemDto>
{
    public CreateTaskItemDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title cannot be empty")
            .MinimumLength(3).WithMessage("Title must be at least 3 characters long");
    }
}