namespace TABP.API.Extensions.DependencyInjection;

internal static class UtilityServicesRegistration
{
    public static IServiceCollection RegisterUtilites(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}