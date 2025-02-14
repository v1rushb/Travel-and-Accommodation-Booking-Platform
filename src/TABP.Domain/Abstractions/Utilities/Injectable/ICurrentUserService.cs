using TABP.Domain.Enums;

namespace TABP.Domain.Abstractions.Utilities.Injectable;

/// <summary>
/// Provides access to the current authenticated user's details, such as their ID and roles.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Retrieves the unique identifier of the current authenticated user.
    /// </summary>
    /// <returns>The user ID as a<see cref="Guid"/>.</returns>\
    /// <exception cref="UnauthorizedAccessException">Thrown if the user is not authenticated.</exception>
    Guid GetUserId();


    /// <summary>
    /// Retrieves the list of roles assigned to the current authenticated user.
    /// </summary>
    /// <returns>A list of roles (<see cref="RoleType"/>) assigned to the user.</returns>
    List<RoleType> GetUserRoles();

     /// <summary>
    /// Checks whether the current authenticated user has a specified role.
    /// </summary>
    /// <param name="roleType">The role to check.</param>
    /// <returns><c>true</c> if the user is in the specified role; otherwise, <c>false</c>.</returns>
    bool IsInRole(RoleType roleType);
}