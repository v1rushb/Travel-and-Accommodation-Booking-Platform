using System.Runtime.CompilerServices;
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
    public async Task<Role> GetByNameAsync(string roleName)
    {
        var role = await _context.Roles
            .FirstOrDefaultAsync(role => role.Name == roleName);
        
        return role ?? await AddAsync(roleName);
    }

    private async Task<Role> AddAsync(string roleName)
    {
        var newRole = new Role
        {
            Name = roleName,
            Description = String.Empty // empty for now.
        };

        _context.Roles.Add(newRole);
        await _context.SaveChangesAsync();

        return newRole;
    }


}