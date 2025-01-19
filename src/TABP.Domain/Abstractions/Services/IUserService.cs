using TABP.Domain.Models.User;

namespace TABP.Domain.Abstractions.Services;

public interface IUserService
{
    Task<Guid> CreateAsync(UserDTO newUser);

    Task<string> AuthenticateAsync(UserLoginDTO userLoginCredentials);
    Task<bool> ExistsAsync(Guid Id);
    Task<bool> ExistsByUsernameAsync(string username);
}