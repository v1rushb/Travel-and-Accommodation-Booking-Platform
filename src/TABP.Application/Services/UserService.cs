using FluentValidation;
using Microsoft.AspNetCore.Identity;
using TABP.Abstractions.Repositories;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Exceptions;
using TABP.Domain.Models.User;

namespace TABP.Appllication.Services;


// very dirty, clean asap.
// missing all kinds of validations.
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IPasswordHasher<string> _passwordHasher;
    private readonly IValidator<UserDTO> _userValidator;

    public UserService(
        IUserRepository userRepository,
        ITokenGenerator tokenGenerator,
        IPasswordHasher<string> passwordHasher,
        IRoleRepository roleRepository,
        IValidator<UserDTO> userValidator)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
        _roleRepository = roleRepository;
        _userValidator = userValidator;
    }

    public async Task<Guid> CreateAsync(UserDTO newUser)
    {
        await _userValidator.ValidateAndThrowAsync(newUser);

        newUser.Password = _passwordHasher.HashPassword(
            newUser.Username,
            newUser.Password
        );
        
        var role = await _roleRepository.GetByNameAsync(nameof(RoleType.Admin));
        newUser.Roles.Add(role); // should never be null

        return await _userRepository.AddAsync(newUser);
    }

    public async Task<string> AuthenticateAsync(UserLoginDTO userLoginCredentials) // mayeb add fluent validation here?
    {
        var storedUser = await ValidateUsername(userLoginCredentials.Username);
        ValidatePassword(
            userLoginCredentials.Password,
            storedUser.Password,
            userLoginCredentials.Username);

        return _tokenGenerator.GenerateToken(storedUser);
    }

    private async Task<UserDTO> ValidateUsername(string loginUsername)
    {
        var storedUser = await _userRepository.GetByUsernameWithRolesAsync(loginUsername);

        return storedUser ?? 
            throw new InvalidUserCredentialsException();
    }

    private void ValidatePassword(string loginPassword, string storedPassword, string username)
    {
        var validationResult = _passwordHasher.VerifyHashedPassword( 
            username,
            storedPassword,
            loginPassword
        );

        if(validationResult == PasswordVerificationResult.Failed)
        {
            throw new InvalidUserCredentialsException();
        }
    }
    
    public async Task<bool> ExistsAsync(Guid Id) =>
        await _userRepository.ExistsAsync(Id);

    public async Task<bool> ExistsByUsernameAsync(string username) =>
        await _userRepository.ExistsByUsernameAsync(username);
}