using System.Net;
using AutoFixture;
using FluentAssertions;
using Xunit;

namespace TABP.API.IntegrationTests;

public class HotelControllerTests : IClassFixture<HotelBookingFactory>, IAsyncLifetime
{
    private readonly HotelBookingFactory _factory;
    private HttpClient _guest;
    private HttpClient _user;
    private HttpClient _admin;
    private HttpClient _loggedOutUser;
    private HttpClient _loggedOutAdmin;
    private readonly Fixture _fixture = new();
    
    public HotelControllerTests(HotelBookingFactory factory)
    {
        _factory = factory;
    }

    public async Task InitializeAsync()
    {
        _guest = await _factory.GetGuestClient();
        _user = await _factory.GetAuthenticatedUserClientAsync();
        _admin = await _factory.GetAuthenticatedAdminClientAsync();
        _loggedOutUser = await _factory.GetLoggedOutUserClientAsync();
        _loggedOutAdmin = await _factory.GetLoggedOutAdminClientAsync();
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task Requests_ReturnsUnauthorizedFor_Guests()
    {
        await ExecuteUserRequestTests(_guest, HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task Requests_ShouldBeAuthorizedFor_Admins()
    {
        await ExecuteUserRequestTests(_admin, isGuest: false);
    }

    [Fact]
    public async Task Requests_ShouldBeAuthorizedFor_Users()
    {
        await ExecuteUserRequestTests(_user, isGuest: false);
    }

    private async Task ExecuteUserRequestTests(
        HttpClient client,
        HttpStatusCode? expectedStatusCode = null,
        bool isGuest = true)
    {
        // Arrange
        var searchTerm = string.Empty;
        var hotelId = new Guid();

        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client
                .GetAsync($"api/hotels/search?query={searchTerm}"))
                    .StatusCode;
        var getPageStatusCode = (await client
                .GetAsync($"api/hotels/{hotelId}/page"))
                    .StatusCode;

        var getFeaturedStatusCode = (await client
                .GetAsync("api/hotels/featured"))
                    .StatusCode;

        statusCodes.AddRange(
            [searchStatusCode, getFeaturedStatusCode, getPageStatusCode]);

        // Assert
        if(isGuest) {
            statusCodes.ForEach(statusCode => 
                statusCode
                    .Should().Be(expectedStatusCode));
        } else {
            statusCodes.ForEach(statusCode =>
                statusCode.Should().NotBe(HttpStatusCode.Unauthorized)
                    .And.NotBe(HttpStatusCode.Forbidden));
        }
    }

    [Fact]
    public async Task Requests_ReturnForbiddenFor_LoggedOutUsers()
    {
        await ExecuteLoggedOutRequestTests(_loggedOutUser);
    }

    [Fact]
    public async Task Requests_ReturnForbiddenFor_LoggedOutAdmins()
    {
        await ExecuteLoggedOutRequestTests(_loggedOutAdmin);
    }

    private async Task ExecuteLoggedOutRequestTests(
        HttpClient client)
    {
         // Arrange
        var searchTerm = string.Empty;
        var hotelId = new Guid();

        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client
                .GetAsync($"api/hotels/search?query={searchTerm}"))
                    .StatusCode;
        var getPageStatusCode = (await client
                .GetAsync($"api/hotels/{hotelId}/page"))
                    .StatusCode;

        var getFeaturedStatusCode = (await client
                .GetAsync("api/hotels/featured"))
                    .StatusCode;

        statusCodes.AddRange(
            [searchStatusCode, getFeaturedStatusCode, getPageStatusCode]);

        // Assert
        statusCodes.ForEach(statusCode => statusCode
            .Should().Be(HttpStatusCode.Forbidden)
                .And.NotBe(HttpStatusCode.Unauthorized));
    }

    [Fact]
    public async Task Search_ShouldReturnOkFor_Users()
    {
        // Arrange
        var searchTerm = _fixture.Create<string>();


        // Act
        var statusCode = (await _user
            .GetAsync($"/api/hotels/search?searchTerm={searchTerm}"))
                .StatusCode;


        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
    }
}