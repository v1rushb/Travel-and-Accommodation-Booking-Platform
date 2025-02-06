using TABP.Domain.Constants;
using TABP.Domain.Exceptions;

public class EmptyCartException : CustomException
{
    public EmptyCartException(string message)
        : base(message, CustomExceptionMessages.EmptyCart.Title)
    { }
}