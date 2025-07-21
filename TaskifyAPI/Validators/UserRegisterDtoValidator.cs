using FluentValidation;
using TaskifyAPI.Dtos;

namespace TaskifyAPI.Validators;

public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegisterDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(50).WithMessage("Username must not exceed 50 characters")
            .MinimumLength(3).WithMessage("Username must not exceed 3 characters");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is invalid")
            .MaximumLength(255).WithMessage("Email is too long")
            .MinimumLength(3).WithMessage("Email is too short");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(3).WithMessage("Password must not exceed 3 characters")
            .Matches("[A-Z]").WithMessage("The password should contain at least 1 uppercase character")
            .Matches("[a-z]").WithMessage("The password should contain at least 1 lowercase character")
            .Matches("[0-9]").WithMessage("The password should contain 1 number")
            .Matches("[^a-zA-Z0-9]").WithMessage("The password should contain at least 1 special character");

    }
}