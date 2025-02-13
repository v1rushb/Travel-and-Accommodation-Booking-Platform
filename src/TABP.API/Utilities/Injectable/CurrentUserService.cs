using System.Security.Claims;
using TABP.Domain.Enums;
using TABP.Domain.Abstractions.Utilities.Injectable;

namespace TABP.API.Utilities.Injectable;

internal class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ??
            throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public Guid GetUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(claim => claim.Type == "name");
        if(userIdClaim == null)
            throw new UnauthorizedAccessException("User is not authenticated.");
        
        return Guid.Parse(userIdClaim.Value); // maybe do try parse? not needed for now.
    }

    public List<RoleType> GetUserRoles()
    {
        return _httpContextAccessor.HttpContext?.User?.Claims
            .Where(claim => claim.Type == ClaimTypes.Role)
            .Select(claim => Enum.TryParse<RoleType>(claim.Value, true, out var roleType) ? roleType : (RoleType?)null)
            .Select(roleType => roleType.Value)
            .ToList() ?? [];
    }

    public bool IsInRole(RoleType roleType)
    {
        var roleName = roleType.ToString();
        return _httpContextAccessor.HttpContext?.User?.Claims
            .Any(claim => claim.Type == ClaimTypes.Role && claim.Value.Equals(roleName, StringComparison.OrdinalIgnoreCase)) ?? false; 
    }
}