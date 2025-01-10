using TABP.Domain.Constants;

namespace TABP.Domain.Exceptions;

public class NotFoundException(string message) : CustomException(message)
{
    public override string Title => CustomExceptionMessages.NotFound.Title;
}