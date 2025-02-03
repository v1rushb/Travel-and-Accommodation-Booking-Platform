using TABP.Domain.Constants;
using TABP.Domain.Exceptions;

public class EmailSendingException : CustomException
{
    public EmailSendingException(string message)
        : base(message, CustomExceptionMessages.EmailSending.Title)
    { }
}