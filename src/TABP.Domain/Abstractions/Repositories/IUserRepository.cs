using TABP.Domain.Models.User;

namespace TABP.Domain.Abstractions.Repositories;

public interface IUserRepository
{
    Task<Guid> CreateUserAsync(UserDTO newUser);

    Task<UserDTO> GetByIdAsync(Guid Id);

    Task<bool> ExistsAsync(Guid Id);

    Task<bool> UsernameExistsAsync(string username);

    Task<UserDTO> GetByUsernameAsync(string username);
}