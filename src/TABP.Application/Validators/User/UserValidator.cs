using FluentValidation;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Constants.User;
using TABP.Domain.Models.User;

namespace TABP.Application.Validators.User;

internal class UserValidator : AbstractValidator<UserDTO>
{
    public UserValidator(IUserRepository userRepository)
    {
        RuleFor(user => user.Username)
            .NotNull()
            .Length(UserConstants.MinUsernameLength, UserConstants.MaxUsernameLength)
            .Matches(@"^[a-zA-Z0-9_]+$")
                    .WithMessage("{PropertyName} must be alphanumeric (underscores allowed).")
            .WithMessage("{PropertyName} has invalid length or format.")
            .MustAsync(async (username, cancellation) => 
                !await userRepository.ExistsByUsernameAsync(username))
            .WithMessage("{PropertyName} already exists.");

        RuleFor(user => user.FirstName)
            .NotNull()
            .Length(UserConstants.MinFirstNameLength, UserConstants.MaxFirstNameLength)
            .WithMessage("{PropertyName} has invalid length or format.")
            .Matches(@"^[A-Za-z]+$")
                    .WithMessage("{PropertyName} must contain only letters.");

        RuleFor(user => user.LastName)
            .NotNull()
            .Length(UserConstants.MinLastNameLength, UserConstants.MaxLastNameLength)
            .WithMessage("{PropertyName} has invalid length or format.")
            .Matches(@"^[A-Za-z]+$")
                    .WithMessage("{PropertyName} must contain only letters.");

        RuleFor(user => user.Password)
            .NotNull()
            .Length(UserConstants.MinPasswordLength, UserConstants.MaxPasswordLength)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]).+$")
                    .WithMessage("{PropertyName} must have at least one uppercase letter, one lowercase letter, one digit, and one special character.");

        RuleFor(user => user.Email)
            .NotNull()
            .EmailAddress();
    }
}