using TABP.Domain.Constants;

namespace TABP.Domain.Exceptions;

public class ConfigurationException : CustomException
{
    public ConfigurationException(string message)
        : base(message, CustomExceptionMessages.Configuration.Title)
    { }
}