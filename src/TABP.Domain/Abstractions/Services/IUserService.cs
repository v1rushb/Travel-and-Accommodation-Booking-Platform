using TABP.Domain.Models.User;

namespace TABP.Domain.Abstractions.Services;

public interface IUserService
{
    Task<Guid> AddUserAsync(UserDTO newUser);

    Task<bool> ExistsAsync(Guid Id);

    Task<string> AuthenticateAsync(UserLoginDTO userLoginCredentials);
}