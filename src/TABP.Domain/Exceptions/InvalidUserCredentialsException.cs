namespace TABP.Domain.Exceptions;

public class InvalidUserCredentialsException : CustomException
{
    public InvalidUserCredentialsException() 
        : base("Invalid credentials provided.") { }
}