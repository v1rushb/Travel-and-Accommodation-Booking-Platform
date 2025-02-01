using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IBlacklistService _blacklistService;

    public AuthenticationController(
        IUserService userService,
        ILogger<AuthenticationController> logger,
        IMapper mapper,
        IBlacklistService blacklistService)
    {
        _userService = userService;
        _logger = logger;
        _mapper = mapper;
        _blacklistService = blacklistService;
    }
    
    //Incomplete
    [HttpPost("user-register")]
    public async Task<IActionResult> RegisterUserAsync(UserRegisterationDTO newUser)
    {
        await _userService
            .CreateAsync(_mapper.Map<UserDTO>(newUser));

        _logger.LogInformation("User {Username} has been registered successfully", newUser.Username);

        return Created();
    }

    [HttpPost("user-login")]
    public async Task<IActionResult> LoginUserAsync(UserLoginDTO userLoginCredentials)
    {
        var authenticationToken = await _userService
            .AuthenticateAsync(userLoginCredentials);

        _logger.LogInformation("User {Username} has logged in successfully", userLoginCredentials.Username);

        return Ok(authenticationToken);
    }

    [HttpPost("user-logout")] //refactor later.
    [Authorize]
    public async Task<IActionResult> LogoutUserAsync()
    {
        var authHeader = Request.Headers.Authorization.FirstOrDefault();

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            return Unauthorized(new { Message = "Invalid authorization header" });
        }
        var token = authHeader["Bearer ".Length..].Trim(); // check later.

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var expUnixTime = long.Parse(jwtToken.Claims.First(claim => claim.Type == "exp").Value);
        var expirationTime = DateTimeOffset.FromUnixTimeSeconds(expUnixTime);
        var remainingTime = expirationTime - DateTimeOffset.UtcNow;

        await _blacklistService.AddToBlacklistAsync(token, remainingTime);

        return Ok();
    }
}