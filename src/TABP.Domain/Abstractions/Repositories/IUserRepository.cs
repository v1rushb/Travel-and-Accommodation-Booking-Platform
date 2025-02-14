using TABP.Domain.Models.User;

namespace TABP.Domain.Abstractions.Repositories;

/// <summary>
/// Defines the contract for a repository to manage <see cref="User"/> entities.
/// This interface provides asynchronous operations for creating, retrieving, updating user data,
/// as well as checking for user existence by ID or username, and fetching users by username with their roles.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Asynchronously adds a new user to the repository.
    /// </summary>
    /// <param name="newUser">A <see cref="UserDTO"/> containing the data for the new user.</param>
    /// <returns>A <see cref="Task{Guid}"/> representing the asynchronous operation, and upon completion,
    /// returns the unique identifier of the newly added user.
    /// </returns>
    Task<Guid> AddAsync(UserDTO newUser);

    /// <summary>
    /// Asynchronously retrieves a user from the repository by their unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the user to retrieve.</param>
    /// <returns>A <see cref="Task{UserDTO}"/> representing the asynchronous operation, and upon completion,
    /// returns the <see cref="UserDTO"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<UserDTO> GetByIdAsync(Guid Id);

    /// <summary>
    /// Asynchronously checks if a user with the specified ID exists in the repository.
    /// </summary>
    /// <param name="Id">The unique identifier of the user to check.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion,
    /// returns <c>true</c> if a user with the given ID exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsAsync(Guid Id);

    /// <summary>
    /// Asynchronously checks if a user with the specified username exists in the repository.
    /// </summary>
    /// <param name="username">The username of the user to check.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion,
    /// returns <c>true</c> if a user with the given username exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsByUsernameAsync(string username);

    /// <summary>
    /// Asynchronously retrieves a user from the repository by their username, including their roles.
    /// </summary>
    /// <param name="username">The username of the user to retrieve.</param>
    /// <returns>A <see cref="Task{UserDTO}"/> representing the asynchronous operation, and upon completion,
    /// returns the <see cref="UserDTO"/> if found, including their roles; otherwise, <c>null</c>.
    /// </returns>
    Task<UserDTO> GetByUsernameWithRolesAsync(string username);

    /// <summary>
    /// Asynchronously updates an existing user in the repository.
    /// </summary>
    /// <param name="user">A <see cref="UserDTO"/> containing the updated data for the user.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateAsync(UserDTO user);

}