using TABP.Domain.Constants;

namespace TABP.Domain.Exceptions;

public class ConfigurationException(string message) : CustomException(message)
{
    public override string Title => CustomExceptionMessages.ConfigurationException.Title;
}
