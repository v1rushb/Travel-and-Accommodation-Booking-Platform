using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TABP.Domain.Abstractions.Utilities.Injectable;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using TABP.API.Utilities.Injectable;
using TABP.Domain.Models.Configurations;

namespace TABP.API.Extensions.DependencyInjection;

internal static class APIServices
{
    public static IServiceCollection RegisterAuthentication(this IServiceCollection services, IConfiguration config)
    {
        var jwtConfig = new JWTConfigurations();
        config.GetSection(nameof(JWTConfigurations))
        .Bind(jwtConfig);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                SetTokenValidationParameters(options, jwtConfig);
            });
            
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.AddControllers().AddNewtonsoftJson();

        return services;
    }

    public static void SetTokenValidationParameters(
        JwtBearerOptions options, JWTConfigurations jwtConfig
    )
    {
        options.TokenValidationParameters = new()
        {
            NameClaimType = JwtRegisteredClaimNames.Name,
            RoleClaimType = ClaimTypes.Role,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidAudience = jwtConfig.Audience,
            ValidIssuer = jwtConfig.Issuer,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtConfig.Key)),
            ClockSkew = TimeSpan.Zero
        };
    }

    public static void AddLoggingService(this IHostBuilder builder)
    {
         builder.UseSerilog((context, config) =>
            config.ReadFrom.Configuration(context.Configuration));
    }

    public static void AddRateLimitingService(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.AddPolicy(nameof(RateLimitingPolicies.FixedWindow), httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(), // hmm maybe consider the case of localhost and nginx?
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 10,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        Window = TimeSpan.FromSeconds(3),
                        QueueLimit = 4
                    }
                )
            );
        });
    }
}