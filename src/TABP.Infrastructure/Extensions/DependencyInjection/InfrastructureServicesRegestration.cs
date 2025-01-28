using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using TABP.Abstractions.Repositories;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Configurations;
using TABP.Infrastructure.Cache;
using TABP.Infrastructure.Persistence;
using TABP.Infrastructure.Repositories;
using TABP.Infrastructure.Utilities;

namespace TABP.Infrastructure.Extensions.DependencyInjection;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
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
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IImageRepository, ImageRepository>();
        AddCache(services, configuration);
        services.AddHostedService<RedisCacheEventService>();
        services.AddSingleton<IBlacklistService, BlacklistService>();
        services.AddTransient<ICacheEventService, RedisCacheEventService>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        AddEmailServices(services, configuration);

        return services;
    }

    private static IServiceCollection AddCache(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string redisConnectionString = configuration.GetConnectionString("Redis");

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnectionString;  
        });

        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(redisConnectionString));

        

        return services;
    }

    public static IServiceCollection AddEmailServices(
    this IServiceCollection services, 
    IConfiguration configuration)
    {
        var emailSettings = configuration.GetSection(nameof(EmailSettings)).Get<EmailSettings>();
        
        if (emailSettings?.SMTPSettings == null)
        {
            throw new InvalidOperationException("EmailSettings configuration is missing or invalid");
        }

        services.AddFluentEmail(emailSettings.DefaultFromEmail)
            .AddSmtpSender(new System.Net.Mail.SmtpClient
            {
                Host = emailSettings.SMTPSettings.Host,
                Port = emailSettings.SMTPSettings.Port,
                EnableSsl = emailSettings.SMTPSettings.EnableSsl,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(
                    emailSettings.SMTPSettings.User,
                    emailSettings.SMTPSettings.Password
                )
            })
            .AddRazorRenderer();

        return services;
    }

}