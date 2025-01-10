using TABP.Domain.Constants;

namespace TABP.Domain.Exceptions;

public class BadRequestException(string message) : CustomException(message)
{
    public override string Title => CustomExceptionMessages.BadRequest.Title;
}