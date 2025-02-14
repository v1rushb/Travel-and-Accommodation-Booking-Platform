using TABP.Domain.Entities;

namespace TABP.Domain.Abstractions.Repositories;

/// <summary>
/// Defines the contract for a repository to manage <see cref="Role"/> entities.
/// This interface provides asynchronous operations for retrieving role data based on the role name.
/// </summary>
public interface IRoleRepository
{
    /// <summary>
    /// Asynchronously retrieves a role from the repository by its name, if it does not exist, it creates and returns it.
    /// </summary>
    /// <param name="roleName">The name of the role to retrieve.</param>
    /// <returns>A <see cref="Task{Role}"/> representing the asynchronous operation, and upon completion,
    /// returns the <see cref="Role"/> if found; otherwise, creates an returns it.</returns>
    Task<Role> GetByNameAsync(string roleName);
}