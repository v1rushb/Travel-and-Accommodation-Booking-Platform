namespace TABP.Domain.Exceptions;

public abstract class CustomException : Exception
{
    public string Title { get; }
    protected CustomException(string message, string title, Exception exception = null) 
        : base(message, exception)
    {
        Title = title;
    }

    protected CustomException(string title)
        : base() 
    {
        Title = title;
    }
}