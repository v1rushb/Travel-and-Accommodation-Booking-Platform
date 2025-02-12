using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using TABP.Application.Services;
using TABP.Application.Services.Booking;
using TABP.Application.Services.Cart;
using TABP.Application.Services.City;
using TABP.Application.Services.Review;
using TABP.Application.Services.Hotel;
using TABP.Application.Vaildator.Images;
using TABP.Application.Validators.Booking;
using TABP.Application.Validators.Cart;
using TABP.Application.Validators.City;
using TABP.Application.Validators.Discount;
using TABP.Application.Validators.Hotel;
using TABP.Application.Validators.Image;
using TABP.Application.Validators.Pagination;
using TABP.Application.Validators.Review;
using TABP.Application.Validators.User;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Abstractions.Services.Booking;
using TABP.Domain.Abstractions.Services.Cart;
using TABP.Domain.Abstractions.Services.City;
using TABP.Domain.Abstractions.Services.Hotel;
using TABP.Domain.Abstractions.Services.Review;
using TABP.Domain.Models.CartItem;
using TABP.Domain.Models.City;
using TABP.Domain.Models.Discount;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.HotelReview;
using TABP.Domain.Models.Image;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Room;
using TABP.Domain.Models.RoomBooking;
using TABP.Domain.Models.User;
using TABP.Application.Services.Room;
using TABP.Application.Validators.Room;
using TABP.Domain.Models.HotelVisit;
using TABP.Application.Validators.TimeOption;

namespace TABP.Application.Extensions.DependencyInjection;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        AddUserServiceDependencies(services);

        AddHotelServices(services);

        AddRoomServices(services);

        AddCityServices(services);

        AddReviewServices(services);

        AddBookingServices(services);

        AddCartServices(services);

        services.AddScoped<ICartItemService, CartItemService>();
        services.AddScoped<IDiscountService, DiscountService>();
        services.AddScoped<IHotelVisitService, HotelVisitService>();
        services.AddScoped<IImageService, ImageService>();

        AddValidators(services);

        return services;
    }

    private static void AddHotelServices(this IServiceCollection services)
    {
        services.AddScoped<IHotelAdminService, HotelAdminService>();
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IHotelUserService, HotelUserService>();
        services.AddScoped<IHotelVisitService, HotelVisitService>();
    }

    private static void AddRoomServices(this IServiceCollection services)
    {
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IRoomAdminService, RoomAdminService>();
        services.AddScoped<IRoomUserService, RoomUserService>();
        services.AddScoped<IRoomImageService, RoomImageService>();
    }

    private static void AddCityServices(this IServiceCollection services)
    {
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<ICityAdminService, CityAdminService>();
        services.AddScoped<ICityUserService, CityUserService>();
        services.AddScoped<ICityImageService, CityImageService>();
    }

    private static void AddReviewServices(this IServiceCollection services)
    {
        services.AddScoped<IHotelReviewService, HotelReviewService>();
        services.AddScoped<IHotelReviewAdminService, HotelReviewAdminService>();
        services.AddScoped<IHotelReviewUserService, HotelReviewUserService>();
    }

    private static void AddBookingServices(this IServiceCollection services)
    {
        services.AddScoped<IRoomBookingService, RoomBookingService>();
        services.AddScoped<IRoomBookingAdminService, RoomBookingAdminService>();
        services.AddScoped<IRoomBookingUserService, RoomBookingUserService>();
        services.AddScoped<IRoomBookingEmailService, RoomBookingEmailService>(); 
    }

    private static void AddCartServices(this IServiceCollection services)
    {
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<ICartAdminService, CartAdminService>();
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
        services.AddScoped<IValidator<CartItemDTO>, CartItemValidator>();
        services.AddScoped<IValidator<PaginationDTO>, PaginationValidator>();
        services.AddScoped<IValidator<ImageSizeDTO>, ImageSizeValidator>();
        services.AddScoped<IValidator<IEnumerable<Image>>, ImageValidator>();
        services.AddScoped<IValidator<VisitTimeOptionQuery>, TimeOptionsValidator>();
    }
}