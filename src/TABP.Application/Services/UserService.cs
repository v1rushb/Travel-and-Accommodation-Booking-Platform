using Microsoft.AspNetCore.Identity;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Exceptions;
using TABP.Domain.Models.User;

namespace TABP.Appllication.Services;


// very dirty, clean asap.
// missing all kinds of validations.
// use IValidator.
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IPasswordHasher<string> _passwordHasher;

    public UserService(
        IUserRepository userRepository,
        ITokenGenerator tokenGenerator,
        IPasswordHasher<string> passwordHasher)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> CreateUserAsync(UserDTO newUser)
    {
        if(await _userRepository.ExistsAsync(newUser.Id)) // do something more proper
        {
            throw new UserDuplicateException(newUser.Id);
        }

        newUser.Password = _passwordHasher.HashPassword(
            newUser.Username,
            newUser.Password
        );

        return await _userRepository.CreateUserAsync(newUser);
    }

    public async Task<string> AuthenticateAsync(UserLoginDTO userLoginCredentials)
    {
        var storedUser = await ValidateUsername(userLoginCredentials.Username);
        ValidatePassword(
            userLoginCredentials.Password,
            userLoginCredentials.Password,
            userLoginCredentials.Username);

        return _tokenGenerator.GenerateToken(storedUser);
    }

    private async Task<UserDTO> ValidateUsername(string loginUsername)
    {
        var storedUser = await _userRepository.GetByUsernameAsync(loginUsername);

        return storedUser ?? 
            throw new InvalidUserCredentialsException();
    }

    private void ValidatePassword(string loginPassword, string storedPassword, string username)
    {
        var validationResult = _passwordHasher.VerifyHashedPassword( 
            username,
            loginPassword,
            storedPassword
        );

        if(validationResult == PasswordVerificationResult.Failed)
        {
            throw new InvalidUserCredentialsException();
        }
    }
}