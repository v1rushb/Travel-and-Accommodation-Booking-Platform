using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Configurations;
using Microsoft.Extensions.Options;
using TABP.Domain.Models.User;
using TABP.Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using FluentValidation;

namespace TABP.Application.Services;

public class TokenGenerator : ITokenGenerator
{
    private readonly JWTConfigurations _jwtConfig;
    private readonly IValidator<JWTConfigurations> _jwtConfigValidator;

    public TokenGenerator(
        IOptions<JWTConfigurations> jwtOptions,
        IValidator<JWTConfigurations> jwtConfigValidator)
    {
        _jwtConfig = jwtOptions.Value ??
            throw new MissingConfigurationException(nameof(JWTConfigurations));
        _jwtConfigValidator = jwtConfigValidator;
        
    }

    public string GenerateToken(UserDTO user)
    {
        try {
            _jwtConfigValidator.ValidateAndThrow(_jwtConfig);
        } catch (ValidationException ex)
        {
            var errorMessages = string.Join("; ", ex.Errors.Select(err => err.ErrorMessage));

            throw new InvalidJWTConfigurationException($"Invalid JWT Configuration: {errorMessages}");
        }
        
        var signingCredentials = GetSigningCredentials();
        var claims = GetTokenClaims(user);

        var token = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtConfig.ExpirationTimeMinutes),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private SigningCredentials GetSigningCredentials()
    {
        try {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            return credentials;
        } catch (Exception ex)
        {
            throw new TokenGenerationException("Failed to generate signing credentials for JWT.", ex);
        }

    }

    private List<Claim> GetTokenClaims(UserDTO user) // make some dto
    {

        var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Name, user.Id.ToString())
            };
        var userRoles = user.Roles;
        if (userRoles != null)
        {
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
        }

        return claims;
    }
}