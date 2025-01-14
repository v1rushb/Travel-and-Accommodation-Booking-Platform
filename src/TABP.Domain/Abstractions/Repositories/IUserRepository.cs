using TABP.Domain.Entities;
using TABP.Domain.Models.User;
using TABP.Domain.Models.Users;

namespace TABP.Domain.Abstractions.Repositories;

public interface IUserRepository
{
    Task<Guid> AddAsync(UserDTO newUser);

    Task<UserDTO> GetByIdAsync(Guid Id);

    Task<bool> ExistsAsync(Guid Id);

    Task<bool> UsernameExistsAsync(string username);

    Task<UserDTO> GetByUsernameWithRolesAsync(string username);

}