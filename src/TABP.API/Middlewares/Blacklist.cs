using TABP.Domain.Abstractions.Services;

namespace TABP.API.Middlewares;

public class Blacklist
{
    private readonly RequestDelegate _next;
    private readonly IBlacklistService _blacklistService;
    private readonly ILogger<Blacklist> _logger;

    public Blacklist(
        RequestDelegate next,
        IBlacklistService blacklistService,
        ILogger<Blacklist> logger)
    {
        _next = next;
        _blacklistService = blacklistService;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var incomingToken = context.Request.Headers.Authorization
            .FirstOrDefault();

        var token = incomingToken["Bearer ".Length..].Trim();

        if(await _blacklistService.IsTokenBlacklistedAsync(token))
        {
            _logger.LogInformation("Token {Token} is blacklisted. and tried to access the System.", token);
            await context.Response.WriteAsync("Token is blacklisted. Try to login again.");
            return;
        }
        await _next(context);
    }
}