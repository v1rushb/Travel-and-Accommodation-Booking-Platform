using System.Net;
using AutoFixture;
using FluentAssertions;

namespace TABP.API.IntegrationTests;

public class CityControllerTests : IClassFixture<HotelBookingFactory>
{
    private readonly HttpClient _guest;
    private readonly HttpClient _user;
    private readonly HttpClient _admin;
    private readonly HttpClient _loggedOutUser;
    private readonly Fixture _fixture = new();

    public CityControllerTests(HotelBookingFactory factory)
    {
        _guest = factory.GetGuestClient();
        _user = factory.GetAuthenticatedUserClientAsync().Result;
        _admin = factory.GetAuthenticatedAdminClientAsync().Result;
        _loggedOutUser = factory.GetLoggedOutUserClientAsync().Result;
    }

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

    private async Task ExecuteUserRequestTests(
        HttpClient client,
        HttpStatusCode? expectedStatusCode = null,
        bool isGuest = true)
    {
        // Arrange
        var searchTerm = string.Empty;

        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client
                .GetAsync($"api/cities/search?searchTerm={searchTerm}"))
                    .StatusCode;


        statusCodes.AddRange(
            [searchStatusCode]);

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
    public async Task Requests_ReturnForbiddenFor_LoggedOutUser()
    {
        await ExecuteLoggedOutUserRequestTests(_loggedOutUser);
    }

    private async Task ExecuteLoggedOutUserRequestTests(
        HttpClient client)
    {
        // Arrange
        var searchTerm = string.Empty;

        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client
                .GetAsync($"api/cities/search?query={searchTerm}"))
                    .StatusCode;

        statusCodes.AddRange(
            [searchStatusCode]);

        // Assert
        statusCodes.ForEach(statusCode => statusCode
            .Should().Be(HttpStatusCode.Forbidden));
    }

    [Fact]
    public async Task AdminRequests_ShouldBeAuthorizedFor_UserEndpoints()
    {
        await ExecuteAdminRequestTests(_admin);
    }

    private async Task ExecuteAdminRequestTests(
        HttpClient client)
    {
        // Arrange
        var searchTerm = string.Empty;

        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client
                .GetAsync($"api/cities/search?query={searchTerm}"))
                    .StatusCode;

        statusCodes.AddRange(
            [searchStatusCode]);

        // Assert
        statusCodes.ForEach(statusCode =>
            statusCode.Should().NotBe(HttpStatusCode.Forbidden)
                .And.NotBe(HttpStatusCode.Unauthorized));
    }

    [Fact]
    public async Task Search_ShouldReturnOkFor_Users()
    {
        // Arrange
        var searchTerm = _fixture.Create<string>();


        // Act
        var statusCode = (await _user
            .GetAsync($"/api/hotel-reviews/search?searchTerm={searchTerm}"))
                .StatusCode;

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
    }
}