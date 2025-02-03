namespace TABP.Domain.Exceptions;

public class MissingConfigurationException : ConfigurationException
{
    public MissingConfigurationException(string keyName)
        : base($"The required configuration key '{keyName}' is not set or read properly.")
    {
        KeyName = keyName;
    }
    public string KeyName { get; }
}