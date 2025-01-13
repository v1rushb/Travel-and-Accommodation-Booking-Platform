using TABP.Domain.Entities;

namespace TABP.Abstractions.Repositories;

public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string roleName);
    Task<Guid> AddAsync(Role newRole);
}