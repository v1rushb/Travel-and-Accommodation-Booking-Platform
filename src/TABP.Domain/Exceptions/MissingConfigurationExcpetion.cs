namespace TABP.Domain.Exceptions;

public class MissingConfigurationException(string keyName)
    : ConfigurationException($"The required configuration key '{keyName}' is not set or read properly.")
{
    public string KeyName { get; } = keyName;
}
