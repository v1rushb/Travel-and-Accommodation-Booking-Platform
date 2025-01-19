using FluentValidation;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Constants.User;
using TABP.Domain.Models.User;

namespace TABP.Application.Validators.User;

internal class UserValidator : AbstractValidator<UserDTO>
{
    public UserValidator(IUserService userService) // add regex bro.
    {
        RuleFor(user => user.Username)
            .NotNull()
            .Length(UserConstants.MinUsernameLength, UserConstants.MaxUsernameLength)
            .WithMessage("{PropertyName} has invalid length or format.") // maybe give a better message?
            .MustAsync(async (username, cancellation) => 
                !await userService.ExistsByUsernameAsync(username))
            .WithMessage("{PropertyName} already exists.");

        RuleFor(user => user.FirstName)
            .NotNull()
            .Length(UserConstants.MinFirstNameLength, UserConstants.MaxFirstNameLength)
            .WithMessage("{PropertyName} has invalid length or format."); // maybe give a better message?

        RuleFor(user => user.LastName)
            .NotNull()
            .Length(UserConstants.MinLastNameLength, UserConstants.MaxLastNameLength)
            .WithMessage("{PropertyName} has invalid length or format."); // maybe give a better message?

        RuleFor(user => user.Password)
            .NotNull()
            .Length(UserConstants.MinPasswordLength, UserConstants.MaxPasswordLength)
            .WithMessage("{PropertyName} has invalid length or format."); // maybe give a better message? // add regex later.
    }
}