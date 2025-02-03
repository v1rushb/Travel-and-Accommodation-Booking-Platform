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

    public static class Configuration
    {
        public static string Title => "Configuration Error";
    }

    public static class EntityImageLimitExceeded
    {
        public static string Title => "Entity Image Limit Exceeded";
    }

    public static class Cache
    {
        public static string Title => "Cache Operation Failed";
    }

    public static class RedisCache
    {
        public static string Title => "Redis Cache Error";
    }

    public static class EmailSending
    {
        public static string Title => "Email Sending Error";
    }

    public static class InvalidJWTConfiguration
    {
        public static string Title => "JWT Configuration Error";
    }

    public static class InvalidUserCredentials
    {
        public static string Title => "Invalid Credentials";
    }

    public static class UserDuplicate
    {
        public static string Title => "User Duplicate";
    }
}