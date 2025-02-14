using TABP.Domain.Models.User;

namespace TABP.Domain.Abstractions.Services;

/// <summary>
/// Defines operations related to user management, mainly authentication. 
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Creates a new user in the system.
    /// </summary>
    /// <param name="newUser">The user DTO containing user details.</param>
    /// <returns>The unique identifier<see cref="Guid"/>of the newly created user.</returns>
    /// <exception cref="FluentValidation.ValidationException">Thrown if user data validation fails.</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="newUser"/> is null.</exception>
    Task<Guid> CreateAsync(UserDTO newUser);



    /// <summary>
    /// Authenticates a user based on login credentials.
    /// </summary>
    /// <param name="userLoginCredentials">The DTO containing username and password.</param>
    /// <returns>A JWT token string if authentication is successful.</returns>
    /// <exception cref="FluentValidation.ValidationException">Thrown if validation of credentials fails.</exception>
    /// <exception cref="Exceptions.InvalidUserCredentialsException">Thrown if authentication fails due to invalid credentials.</exception>
    Task<string> AuthenticateAsync(UserLoginDTO userLoginCredentials);

    /// <summary>
    /// Checks if a user exists by their unique<see cref="Guid"/>identifier. 
    /// </summary>
    /// <param name="Id">The unique<see cref="Guid"/>identifier of the user.</param>
    /// <returns><c>true</c> if the user exists; otherwise, <c>false</c>.</returns>
    Task<bool> ExistsAsync(Guid Id);

    /// <summary>
    /// Checks if a user exists by their username.
    /// </summary>
    /// <param name="username">The username to check.</param>
    /// <returns><c>true</c> if the user exists; otherwise, <c>false</c>.</returns>
    Task<bool> ExistsByUsernameAsync(string username);
}