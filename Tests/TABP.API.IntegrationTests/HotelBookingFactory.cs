using Microsoft.AspNetCore.Mvc.Testing;
using TABP.Domain.Models.User;
using System.Net.Http.Headers;
using TABP.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;

namespace TABP.API.IntegrationTests;

public class HotelBookingFactory
{
    private readonly WebApplicationFactory<Program> _factory;
    public HotelBookingFactory()
    {
       
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services => 
                {
                    services.AddDbContext<HotelBookingDbContext>();
                });
            });
    }

    public HttpClient GetGuestClient() => _factory.CreateClient();

    public async Task<HttpClient> GetAuthenticatedUserClientAsync()
    {
        var client = _factory.CreateClient();
        var userLoginDTO = new UserLoginDTO
        {
            Username = "user",
            Password = "12345678Aa@_"
        };

        var response = await client.PostAsJsonAsync("/api/auth/user-login", userLoginDTO);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            await response.Content.ReadAsStringAsync());

        return client;
    }

    public async Task<HttpClient> GetAuthenticatedAdminClientAsync()
    {
        var admin = _factory.CreateClient();
        var userLoginDTO = new UserLoginDTO
        {
            Username = "admin",
            Password = "12345678Aa@_"
        };

        var response = await admin.PostAsJsonAsync("/api/auth/user-login", userLoginDTO);
        admin.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            await response.Content.ReadAsStringAsync());

        return admin;
    }

    public async Task<HttpClient> GetLoggedOutAdminClientAsync()
    {
        var client = _factory.CreateClient();
        var userLoginDTO = new UserLoginDTO
        {
            Username = "v1rushb",
            Password = "12345678Aa@_"
        };

        var response = await client.PostAsJsonAsync("/api/auth/user-login", userLoginDTO);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            await response.Content.ReadAsStringAsync());

        await client.PostAsJsonAsync("/api/auth/user-logout", userLoginDTO);
        
        return client;
    }

    public async Task<HttpClient> GetLoggedOutUserClientAsync()
    {
        var client = _factory.CreateClient();
        var userLoginDTO = new UserLoginDTO
        {
            Username = "user2",
            Password = "12345678Aa@_"
        };

        var response = await client.PostAsJsonAsync("/api/auth/user-login", userLoginDTO);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Bearer",
            await response.Content.ReadAsStringAsync());

        await client.PostAsJsonAsync("/api/auth/user-logout", userLoginDTO);
        
        return client;
    }
}