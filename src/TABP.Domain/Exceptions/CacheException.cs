using TABP.Domain.Constants;

namespace TABP.Domain.Exceptions;

 public class CacheException : CustomException
    {
        public CacheException(string message, Exception innerException)
            : base(message, CustomExceptionMessages.Cache.Title, innerException)
        { }
    }