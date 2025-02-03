using FluentValidation;
using TABP.Domain.Constants.JWT;
using TABP.Domain.Models.Configurations;

namespace TABP.Application.Validators.JWT;

public class JWTConfigValidator : AbstractValidator<JWTConfigurations>
{
    public JWTConfigValidator()
    {
        RuleFor(config => config.Key)
            .NotNull()
            .WithMessage("JWT Key must be set.")
            .MinimumLength(JWTConstants.MinKeyLength).WithMessage(
                @$"JWT Key must be at least {JWTConstants.MinKeyLength*8} bits 
                ({JWTConstants.MinKeyLength} characters).");

        RuleFor(config => config.Issuer)
            .NotNull()
            .WithMessage("JWT Issuer must be set.");

        RuleFor(config => config.Audience)
            .NotNull()
            .WithMessage("JWT Audience must be set.");

        RuleFor(config => config.ExpirationTimeMinutes)
            .GreaterThan(JWTConstants.MinExpirationTimeMinutes)
            .WithMessage(
                @$"JWT Expiration time must be greater than 
                    {JWTConstants.MinExpirationTimeMinutes} minutes.");
    }
}