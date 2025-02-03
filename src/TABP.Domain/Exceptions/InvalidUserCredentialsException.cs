using TABP.Domain.Constants;

namespace TABP.Domain.Exceptions;

public class InvalidUserCredentialsException : CustomException
{
    public InvalidUserCredentialsException()
        : base("Invalid credentials provided.", CustomExceptionMessages.InvalidUserCredentials.Title)
    { }
}