using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Configurations;
using Microsoft.Extensions.Options;
using TABP.Domain.Models.User;
using TABP.Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace TABP.Appllication.Services;

public class TokenGenerator : ITokenGenerator
{
    private readonly JWTConfigurations _jwtConfig;

    public TokenGenerator(
        IOptions<JWTConfigurations> jwtOptions)
    {
        _jwtConfig = jwtOptions.Value ??
            throw new MissingConfigurationException(nameof(JWTConfigurations));
    }

    public string GenerateToken(UserDTO user)
    {
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
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        return credentials;
    }

    private List<Claim> GetTokenClaims(UserDTO user) // make some dto
    {

        var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Name, user.Id.ToString())
            };
        var userRoles = user.Roles;
        if(userRoles != null)
        {
            foreach(var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
        }

         return claims;
    }

    // create some validations for the secret key and other options
}