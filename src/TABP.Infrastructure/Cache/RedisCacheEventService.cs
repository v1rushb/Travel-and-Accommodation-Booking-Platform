using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Collections.Concurrent;
using TABP.Domain.Abstractions.Services;

namespace TABP.Infrastructure.Cache;
public class RedisCacheEventService : BackgroundService, ICacheEventService
{
    private readonly IDistributedCache _cache;
    private readonly IConnectionMultiplexer _redis;
    private readonly ILogger<RedisCacheEventService> _logger;
    private readonly ConcurrentDictionary<string, Func<Task>> _expirationCallbacks = new();

    public RedisCacheEventService(
        IDistributedCache cache,
        IConnectionMultiplexer redis,
        ILogger<RedisCacheEventService> logger)
    {
        _cache = cache;
        _redis = redis;
        _logger = logger;
    }

    public async Task ScheduleExpirationAsync(
        string key,
        TimeSpan expiration,
        Func<Task> onExpiredCallback)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));

        await _cache.SetStringAsync(
            key,
            "__expiration_trigger__",
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            }
        );

        _expirationCallbacks[key] = onExpiredCallback;

        _logger.LogInformation(
            "Scheduled expiration for key: {Key} in {ExpirationSeconds} seconds.",
            key,
            expiration.TotalSeconds
        );
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var subscriber = _redis.GetSubscriber();
        await subscriber.SubscribeAsync("__keyevent@0__:expired", async (channel, expiredKey) =>
        {
            var keyStr = expiredKey.ToString();
            _logger.LogInformation("Key {Key} expired.", keyStr);

            if (_expirationCallbacks.TryRemove(keyStr, out var callback))
            {
                try
                {
                    await callback();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error invoking expiration callback for key: {Key}", keyStr);
                }
            }
        });
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("RedisCacheEventService is starting.");

        // var server = _redis.GetServer(_redis.GetEndPoints().First());
        // await server.ConfigSetAsync("notify-keyspace-events", "Ex");

        await base.StartAsync(cancellationToken);
    }
}