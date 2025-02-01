using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;

namespace TABP.Infrastructure.Repositories;

internal class RoleRepository : IRoleRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly ILogger<RoleRepository> _logger;
    public RoleRepository(
        HotelBookingDbContext context,
        ILogger<RoleRepository> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<Role> GetByNameAsync(string roleName)
    {
        var role = await _context.Roles
            .AsTracking()
            .FirstOrDefaultAsync(role => role.Name == roleName);
        
        return role ?? await AddAsync(roleName);
    }

    private async Task<Role> AddAsync(string roleName)
    {
        var newRole = new Role
        {
            Name = roleName,
            Description = roleName
        };

        var entityEntry = _context.Roles.Add(newRole);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Role {RoleName} has been added With Id: {RoleId}",
            newRole.Name,
            entityEntry.Entity.Id
        );

        return newRole;
    }


}