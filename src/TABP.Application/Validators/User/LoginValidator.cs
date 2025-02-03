using FluentValidation;
using TABP.Domain.Models.User;

namespace TABP.Application.Validators.User;

public class LoginValidator : AbstractValidator<UserLoginDTO>
{
    public LoginValidator()
    {
        RuleFor(user => user.Username)
            .NotNull()
            .WithMessage("Username is required.");

        RuleFor(user => user.Password)
            .NotNull()
            .WithMessage("Password is required.");
    }
}