namespace TABP.Domain.Abstractions.Services;

public interface ICacheEventService
{
    Task ScheduleExpirationAsync(string key, TimeSpan expiration, Func<Task> onExpirationCallback);
}