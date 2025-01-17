using TABP.Domain.Enums;

public interface ICurrentUserService
{
    Guid GetUserId();
    List<RoleType> GetUserRoles();
    bool IsInRole(RoleType roleType);
}