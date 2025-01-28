using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Email;
using TABP.Infrastructure.Utilities;

namespace TABP.API.Controllers;

[ApiController]
[Route("api/test")]
public class test : ControllerBase
{
    private readonly ICacheEventService _cacheService;
    private readonly IEmailService _emailService;

    public test(ICacheEventService cacheService, IEmailService emailService)
    {
        _cacheService = cacheService;
        _emailService = emailService;
    }

    [HttpPost("set-expiring-key/{keyName}/{seconds}")]
    public async Task<IActionResult> SetExpiringKey(string keyName, int seconds)
    {
        await _cacheService.ScheduleExpirationAsync(keyName, TimeSpan.FromSeconds(seconds), async () => 
        {
            System.Console.WriteLine("Expired!");
            await Task.CompletedTask;
        });
        
        return NoContent();
    }

     [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromBody] EmailDTO emailDto)
    {
        try
        {
            await _emailService.SendAsync(emailDto);
            return Ok("Email sent successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Failed to send email: {ex.Message}");
        }
    }
}