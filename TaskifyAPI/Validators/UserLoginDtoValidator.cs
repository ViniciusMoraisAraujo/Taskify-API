using System.Data;
using FluentValidation;
using TaskifyAPI.Dtos;

namespace TaskifyAPI.Validators;

public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
{
    public UserLoginDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is invalid");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("PasswordHash is required");
        
    }
}