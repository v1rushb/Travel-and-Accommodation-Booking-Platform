using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.User;

namespace TABP.API.Controllers;

// do exception handling later.
// missing some important logic. do later.

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IMapper _mapper;

    public AuthenticationController(
        IUserService userService,
        ILogger<AuthenticationController> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    
    //Incomplete
    [HttpPost("user-register")]
    public async Task<IActionResult> RegisterUserAsync(UserRegisterationDTO newUser)
    {
        await _userService.CreateUserAsync(_mapper.Map<UserDTO>(newUser));

        _logger.LogInformation("User created"); // meeh?

        return Created();
    }

    [HttpPost("user-login")]
    public async Task<IActionResult> LoginUserAsync(UserLoginDTO userLoginCredentials)
    {
        var authenticationToken = await _userService.AuthenticateAsync(userLoginCredentials);

        _logger.LogInformation($"User {userLoginCredentials.Username} logged in");

        return Ok(authenticationToken);
    }
}