using Finance.Application.Dtos.User;
using FluentValidation;

namespace Finance.Application.Validators.User;

public class RegisterUserValidator : AbstractValidator<UserRegisterDto>
{
    public RegisterUserValidator()
    {
        RuleFor(u => u.Username)
            .Matches("^[a-zA-Z0-9_]+$")
            .WithMessage("Username can only contain letters, numbers, and underscores.");

        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Email is invalid, try again.");

        RuleFor(u => u.Password)
            .NotEmpty()
            .Length(8, 24)
            .WithMessage("Password is invalid, try a different one.");
    }
}