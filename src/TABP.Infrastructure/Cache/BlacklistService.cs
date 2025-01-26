using Microsoft.Extensions.Caching.Distributed;
using TABP.Domain.Abstractions.Services;

namespace TABP.Infrastructure.Cache;

public class BlacklistService : IBlacklistService
{
    private const string _blacklistKeyPrefix = "blacklist:";
    private readonly IDistributedCache _cache;

    public BlacklistService(
        IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task AddToBlacklistAsync(
        string token,
        TimeSpan expiration)
    {
        var prefixedKey = $"{_blacklistKeyPrefix}{token}";
        await _cache.SetStringAsync(
            prefixedKey,
            "yoinky",
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            }
        );

    }

    public async Task<bool> IsTokenBlacklistedAsync(string token)
    {
        var prefixedKey = $"{_blacklistKeyPrefix}{token}";
        var cachedKey = await _cache.GetStringAsync(prefixedKey);

        return cachedKey != null;
    }


}