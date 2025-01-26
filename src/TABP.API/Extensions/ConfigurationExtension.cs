using TABP.Domain.Models.Configurations;

namespace TABP.API.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection BindConfiguration(
        this IServiceCollection services, IConfiguration config)
    {
        services.AddOptions<ConnectionStrings>()
            .Bind(config.GetSection(nameof(ConnectionStrings)))
            .ValidateOnStart();


        services.Configure<JWTConfigurations>(
            config.GetSection(nameof(JWTConfigurations))   
        );

        services.AddOptions<EmailSettings>()
            .Bind(config.GetSection(nameof(EmailSettings)))
            .ValidateOnStart();
        
        return services;
    }
}