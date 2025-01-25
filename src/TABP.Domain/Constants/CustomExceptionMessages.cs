namespace TABP.Domain.Constants;

public static class CustomExceptionMessages
{
    public static string Title => "Exception";
    
    public static class BadRequest
    {
        public static string Title => "Bad Request";
    }

    public static class NotFound
    {
        public static string Title => "Not Found";
    }

    public static class ConfigurationException
    {
        public static string Title => "Configuration Error";
    }

    public static class EntityImageLimitExceededException
    {
        public static string Title => "Entity Image Limit Exceeded";
    }
}