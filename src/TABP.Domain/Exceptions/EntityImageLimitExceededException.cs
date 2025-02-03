using TABP.Domain.Constants;

namespace TABP.Domain.Exceptions;

public class EntityImageLimitExceededException : CustomException
{
    public EntityImageLimitExceededException(string? message = null)
        : base(message ?? string.Empty, CustomExceptionMessages.EntityImageLimitExceeded.Title)
    { }
}
