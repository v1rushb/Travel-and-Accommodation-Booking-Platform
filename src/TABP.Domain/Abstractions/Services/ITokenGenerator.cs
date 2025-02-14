using TABP.Domain.Models.User;

namespace TABP.Domain.Abstractions.Services;

/// <summary>
/// Provides functionality for generating authentication tokens.
/// namely, JWT.
/// </summary>
public interface ITokenGenerator
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the token is to be generated.</param>
    /// <returns>A JWT token as a string.</returns>
    /// <exception cref="Exceptions.InvalidJWTConfigurationException">
    /// Thrown if the JWT configuration is invalid.
    /// </exception>
    /// <exception cref="Exceptions.TokenGenerationException">
    /// Thrown if there is an issue generating the token.
    /// </exception>
    string GenerateToken(UserDTO user);
}