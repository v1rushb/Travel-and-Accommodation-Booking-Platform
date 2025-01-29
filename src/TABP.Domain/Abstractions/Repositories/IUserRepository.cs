using TABP.Domain.Models.User;

namespace TABP.Domain.Abstractions.Repositories;

public interface IUserRepository
{
    Task<Guid> AddAsync(UserDTO newUser);

    Task<UserDTO> GetByIdAsync(Guid Id);

    Task<bool> ExistsAsync(Guid Id);

    Task<bool> ExistsByUsernameAsync(string username);

    Task<UserDTO> GetByUsernameWithRolesAsync(string username);

}