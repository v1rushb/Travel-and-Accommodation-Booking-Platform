using System.Reflection;
using FluentValidation;

namespace TABP.API.Extensions.DependencyInjection;

internal static class UtilityServicesRegistration
{
    public static IServiceCollection RegisterUtilites(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}