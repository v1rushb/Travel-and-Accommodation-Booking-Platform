namespace TABP.Domain.Models.Configurations
{
    public class JWTConfigurations
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationTimeMinutes { get; set; }
    }
}