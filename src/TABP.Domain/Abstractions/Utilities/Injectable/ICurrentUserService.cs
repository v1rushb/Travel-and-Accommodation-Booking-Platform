using TABP.Domain.Enums;

/// <summary>
/// Provides access to the current authenticated user's details, such as their ID and roles.
/// </summary>
namespace TABP.Domain.Abstractions.Utilities.Injectable;

public interface ICurrentUserService
{
    Guid GetUserId();
    List<RoleType> GetUserRoles();
    bool IsInRole(RoleType roleType);
}