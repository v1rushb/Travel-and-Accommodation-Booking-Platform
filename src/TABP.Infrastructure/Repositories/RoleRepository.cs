using Microsoft.EntityFrameworkCore;
using TABP.Abstractions.Repositories;
using TABP.Domain.Entities;

namespace TABP.Infrastructure.Repositories;

internal class RoleRepository : IRoleRepository
{
    private readonly HotelBookingDbContext _context;
    public RoleRepository(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> AddAsync(Role newRole)
    {
        var newRoleRow = new Role
        {
            Name = newRole.Name,
            Description = newRole.Description
        };
        var entityEntry = await _context.Roles.AddAsync(newRoleRow);
        await _context.SaveChangesAsync();

        return entityEntry.Entity.Id;
    }

    public async Task<Role?> GetByNameAsync(string roleName) =>
        await _context.Roles.FirstOrDefaultAsync(role => role.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
}