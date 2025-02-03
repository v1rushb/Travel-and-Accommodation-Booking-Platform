using TABP.Domain.Constants;

namespace TABP.Domain.Exceptions;

public class RedisCacheException : CustomException
{
    public RedisCacheException(string message, Exception innerException)
        : base(message, CustomExceptionMessages.RedisCache.Title, innerException)
    { }
}
