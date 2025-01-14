using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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
        return services;
    }

    private static void SetTokenValidationParameters(
        JwtBearerOptions options, JWTConfigurations jwtConfig
    )
    {
        options.TokenValidationParameters = new()
        {
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