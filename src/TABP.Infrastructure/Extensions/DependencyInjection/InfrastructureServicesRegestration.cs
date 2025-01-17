using Microsoft.Extensions.DependencyInjection;
using TABP.Abstractions.Repositories;
using TABP.Domain.Abstractions.Repositories;
using TABP.Infrastructure.Repositories;

namespace TABP.Infrastructure.Extensions.DependencyInjection;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<HotelBookingDbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<IHotelReviewRepository, HotelReviewsRepository>();
        services.AddScoped<IHotelVisitRepository, HotelVisitRepository>();
        services.AddScoped<IRoomBookingRepository, RoomBookingRepository>();
        services.AddScoped<IRoomRepository, RoomRepository>();

        return services;
    }
}