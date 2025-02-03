using TABP.Domain.Constants;

namespace TABP.Domain.Exceptions;

public class BadRequestException : CustomException
{
    public BadRequestException(string message)
        : base(message, CustomExceptionMessages.BadRequest.Title)
    { }
}