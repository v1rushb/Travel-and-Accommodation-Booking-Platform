using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

    private static void SetTokenValidationParameters(
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
}