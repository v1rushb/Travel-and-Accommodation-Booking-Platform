using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Exceptions;

namespace TABP.Infrastructure.Cache;

public class BlacklistService : IBlacklistService
{
    private const string _blacklistKeyPrefix = "blacklist:";
    private readonly IDistributedCache _cache;
    private readonly ILogger<BlacklistService> _logger;
    public BlacklistService(
        IDistributedCache cache,
        ILogger<BlacklistService> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task AddToBlacklistAsync(
        string token,
        TimeSpan expiration)
    {
        try {
            var prefixedKey = $"{_blacklistKeyPrefix}{token}";
            await _cache.SetStringAsync(
                prefixedKey,
                "yoinky",
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiration
                }
            );
            _logger.LogInformation("A new token has been added to the blacklist");
        } catch (Exception ex)
        {
            throw new CacheException("Failed to add token to blacklist", ex);
        }
    }

    public async Task<bool> IsTokenBlacklistedAsync(string token)
    {
        try {
            var prefixedKey = $"{_blacklistKeyPrefix}{token}";
            var cachedKey = await _cache.GetStringAsync(prefixedKey);

            return cachedKey != null;
        } catch (Exception ex)
        {
            throw new CacheException("Failed to check if token is blacklisted", ex);
        }
    }


}