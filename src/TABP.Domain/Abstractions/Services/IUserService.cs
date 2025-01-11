using TABP.Domain.Models.User;

namespace TABP.Domain.Abstractions.Services;

public interface IUserService
{
    Task<Guid> CreateUserAsync(UserDTO newUser);

    Task<string> AuthenticateAsync(UserLoginDTO userLoginCredentials);
}