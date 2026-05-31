using Finance.Application.Dtos.Auth;
using Finance.Application.Dtos.User;
using FluentValidation;

namespace Finance.Application.Validators.User;

public class UserLoginValidator : AbstractValidator<LoginRequestDto>
{
    public UserLoginValidator()
    {
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