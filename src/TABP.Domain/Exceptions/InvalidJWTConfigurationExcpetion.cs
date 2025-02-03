using TABP.Domain.Constants;
using TABP.Domain.Exceptions;

public class InvalidJWTConfigurationException : CustomException
{
    public InvalidJWTConfigurationException(string message)
        : base(message, CustomExceptionMessages.InvalidJWTConfiguration.Title)
    { }
}