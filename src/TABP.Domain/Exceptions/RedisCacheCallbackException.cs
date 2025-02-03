using TABP.Domain.Constants;

namespace TABP.Domain.Exceptions;

public class RedisCacheCallbackException : CustomException
{
    public RedisCacheCallbackException(string message, Exception innerException)
        : base(message, CustomExceptionMessages.RedisCache.Title, innerException)
    { }
}