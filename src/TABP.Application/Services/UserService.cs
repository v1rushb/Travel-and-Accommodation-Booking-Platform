using FluentValidation;
using Microsoft.AspNetCore.Identity;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Constants.Email;
using TABP.Domain.Enums;
using TABP.Domain.Exceptions;
using TABP.Domain.Models.Email;
using TABP.Domain.Models.User;

namespace TABP.Application.Services;


public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IPasswordHasher<string> _passwordHasher;
    private readonly IValidator<UserDTO> _userValidator;
    private readonly IValidator<UserLoginDTO> _userLoginValidator;
    private readonly IEmailService _emailService;

    public UserService(
        IUserRepository userRepository,
        ITokenGenerator tokenGenerator,
        IPasswordHasher<string> passwordHasher,
        IRoleRepository roleRepository,
        IValidator<UserDTO> userValidator,
        IValidator<UserLoginDTO> userLoginValidator,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
        _roleRepository = roleRepository;
        _userValidator = userValidator;
        _userLoginValidator = userLoginValidator;
        _emailService = emailService;
    }

    public async Task<Guid> CreateAsync(UserDTO newUser)
    {
        await _userValidator.ValidateAndThrowAsync(newUser);

        newUser.Password = _passwordHasher.HashPassword(
            newUser.Username,
            newUser.Password
        );

        var role = await _roleRepository.GetByNameAsync(nameof(RoleType.User));
        newUser.Roles.Add(role); // should never be null


        var id = await _userRepository
            .AddAsync(newUser);

        await SendWelcomeEmailAsync(newUser);
        return id;
    }

    public async Task<string> AuthenticateAsync(UserLoginDTO userLoginCredentials)
    {
        await _userLoginValidator.ValidateAndThrowAsync(userLoginCredentials);

        var storedUser = await ValidateUsername(userLoginCredentials.Username);
        ValidatePassword(
            userLoginCredentials.Password,
            storedUser.Password,
            userLoginCredentials.Username);

        storedUser.LastLogin = DateTime.UtcNow;
        await _userRepository.UpdateAsync(storedUser);

        return _tokenGenerator.GenerateToken(storedUser);
    }

    private async Task SendWelcomeEmailAsync(UserDTO user)
    {
        var body = await ProcessEmailBodyAsync(
            UserEmailConstants.Body,
            user.FirstName
        );

        await _emailService.SendAsync(new EmailDTO
        {
            RecipientEmail = user.Email,
            RecipientName = user.FirstName,
            Subject = UserEmailConstants.Subject,
            Body = body
        });
    }
    private async Task<string> ProcessEmailBodyAsync(string body, string firstName) =>
        body.Replace("{user.FirstName}", firstName);

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

        if (validationResult == PasswordVerificationResult.Failed)
        {
            throw new InvalidUserCredentialsException();
        }
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _userRepository.ExistsAsync(Id);

    public async Task<bool> ExistsByUsernameAsync(string username) =>
        await _userRepository.ExistsByUsernameAsync(username);
}