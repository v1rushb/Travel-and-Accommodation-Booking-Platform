using TABP.Domain.Constants;

namespace TABP.Domain.Exceptions;

public class EntityImageLimitExceededException(string? message = null) : CustomException(message)
{
    public override string Title => 
        CustomExceptionMessages
        .EntityImageLimitExceededException
        .Title;
}
