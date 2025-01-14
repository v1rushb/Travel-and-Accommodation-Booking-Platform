using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TABP.Appllication.Services;
using TABP.Domain.Abstractions.Services;

namespace TABP.Application.Extensions.DependencyInjection;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        AddUserServiceDependencies(services);
        
        return services;
    }

    public static void AddUserServiceDependencies(IServiceCollection services)
    {
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IPasswordHasher<string>, PasswordHasher<string>>();
    }
}