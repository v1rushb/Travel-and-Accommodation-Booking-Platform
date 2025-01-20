using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TABP.Abstractions.Services;
using TABP.Application.Services;
using TABP.Application.Validators.Booking;
using TABP.Application.Validators.City;
using TABP.Application.Validators.Discount;
using TABP.Application.Validators.Hotel;
using TABP.Application.Validators.Review;
using TABP.Application.Validators.Room;
using TABP.Application.Validators.User;
using TABP.Appllication.Services;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.City;
using TABP.Domain.Models.Discount;
using TABP.Domain.Models.HotelReview;
using TABP.Domain.Models.Hotels;
using TABP.Domain.Models.Room;
using TABP.Domain.Models.RoomBooking;
using TABP.Domain.Models.User;

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

        AddValidators(services);

        return services;
    }

    private static void AddUserServiceDependencies(IServiceCollection services)
    {
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IPasswordHasher<string>, PasswordHasher<string>>();
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddScoped<IValidator<UserDTO>, UserValidator>();
        services.AddScoped<IValidator<HotelDTO>, HotelValidator>();
        services.AddScoped<IValidator<DiscountDTO>, DiscountValidator>();
        services.AddScoped<IValidator<RoomDTO>, RoomValidator>();
        services.AddScoped<IValidator<CityDTO>, CityValidator>();
        services.AddScoped<IValidator<HotelReviewDTO>, HotelReviewValidator>();
        services.AddScoped<IValidator<RoomBookingDTO>, BookingValidator>();
    }
}