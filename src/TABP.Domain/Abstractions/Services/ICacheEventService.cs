namespace TABP.Domain.Abstractions.Services;

/// <summary>
/// Defines operations for scheduling cache-related events,
/// such as executing provided callbacks upon expiration.
/// </summary>
public interface ICacheEventService
{
    /// <summary>
    /// Schedules an expiration event to be triggered for a cache entry
    /// after a certain amount of time.
    /// </summary>
    /// <param name="key">The key of the cache entry to monitor.</param>
    /// <param name="expiration">The duration after which the cache entry is considered expired.</param>
    /// <param name="onExpirationCallback">
    /// A callback function to invoke when the cache entry expires.
    /// </param>
    /// <returns>A task that represents the asynchronous scheduling operation.</returns>
    /// <exception cref="ArgumentException">Thrown when the key is null or empty.</exception>
    /// <exception cref="Exceptions.RedisCacheException">Thrown when an error occurs when setting schedule.</exception>
    /// <exception cref="Exceptions.RedisCacheCallbackException">Thrown when a callback can not be invoked.</exception>
    Task ScheduleExpirationAsync(
        string key,
        TimeSpan expiration,
        Func<Task> onExpirationCallback
    );
}