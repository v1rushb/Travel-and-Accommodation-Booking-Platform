using TABP.Domain.Models.Configurations;

namespace TABP.API.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection BindConfiguration(
        this IServiceCollection services, IConfiguration config)
    {
        services.Configure<ConnectionStrings>(
            config.GetSection(nameof(ConnectionStrings))
        );

        services.Configure<JWTConfigurations>(
            config.GetSection(nameof(JWTConfigurations))   
        );
        
        return services;
    }
}