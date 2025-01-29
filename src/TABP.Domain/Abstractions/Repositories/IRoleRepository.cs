using TABP.Domain.Entities;

namespace TABP.Domain.Abstractions.Repositories;

public interface IRoleRepository
{
    Task<Role> GetByNameAsync(string roleName);
}