public class TokenGenerationException : Exception
{
    public TokenGenerationException(string message, Exception exception)
        : base(message, exception) { }
}