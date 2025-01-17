using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TABP.Abstractions.Services;
using TABP.Application.Services;
using TABP.Appllication.Services;
using TABP.Domain.Abstractions.Services;

namespace TABP.Application.Extensions.DependencyInjection;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        AddUserServiceDependencies(services);

        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<ICityService, CityService>();

        services.AddScoped<ICartItemService, CartItemService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IDiscountService, DiscountService>();
        services.AddScoped<IHotelReviewService, HotelReviewService>();
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IHotelVisitService, HotelVisitService>();
        services.AddScoped<IRoomBookingService, RoomBookingService>();
        services.AddScoped<IRoomService, RoomService>();

        
        return services;
    }

    public static void AddUserServiceDependencies(IServiceCollection services)
    {
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IPasswordHasher<string>, PasswordHasher<string>>();
    }
}