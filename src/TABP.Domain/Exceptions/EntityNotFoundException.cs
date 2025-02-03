using TABP.Domain.Constants;

namespace TABP.Domain.Exceptions;

public class EntityNotFoundException : CustomException
{
    public EntityNotFoundException(string message)
        : base(message, CustomExceptionMessages.NotFound.Title)
    {
    }
}