namespace TABP.Domain.Abstractions.Services;

public interface IBlacklistService
{
    Task AddToBlacklistAsync(string token, TimeSpan expiration);
    Task<bool> IsTokenBlacklistedAsync(string token);
}