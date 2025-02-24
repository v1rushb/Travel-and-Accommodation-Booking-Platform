using TABP.API.IntegrationTests;
using System.Net;
using AutoFixture;
using FluentAssertions;


namespace TABP.API.IT;

public class ReviewAdminControllerTests : IClassFixture<HotelBookingFactory>, IAsyncLifetime
{
    private readonly HotelBookingFactory _factory;
    private HttpClient _guest;
    private HttpClient _user;
    private HttpClient _admin;
    private HttpClient _loggedOutAdmin;
    private readonly Fixture _fixture = new();

    public ReviewAdminControllerTests(HotelBookingFactory factory)
    {
        _factory = factory;
    }

    public async Task InitializeAsync()
    {
        _guest = await _factory.GetGuestClient();
        _user = await _factory.GetAuthenticatedUserClientAsync();
        _admin = await _factory.GetAuthenticatedAdminClientAsync();
        _loggedOutAdmin = await _factory.GetLoggedOutAdminClientAsync();
    }

    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task Requests_ReturnsUnauthorizedFor_Guests()
    {
        await ExecuteAdminRequestTests(_guest, HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task Requests_ShouldBeAuthorizedFor_Admins()
    {
        await ExecuteAdminRequestTests(_admin, isGuest: false);
    }

    private async Task ExecuteAdminRequestTests(
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
                .GetAsync($"api/admin/hotel-reviews/search?searchTerm={searchTerm}"))
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
    public async Task Requests_ReturnForbiddenFor_LoggedOutAdmins()
    {
        await ExecuteLoggedOutAdminRequestTests(_loggedOutAdmin);
    }

    private async Task ExecuteLoggedOutAdminRequestTests(
        HttpClient client)
    {
        // Arrange
        var searchTerm = string.Empty;

        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client
                .GetAsync($"api/admin/hotel-reviews/search?query={searchTerm}"))
                    .StatusCode;

        statusCodes.AddRange(
            [searchStatusCode]);

        // Assert
        statusCodes.ForEach(statusCode => statusCode
            .Should().Be(HttpStatusCode.Forbidden));
    }

    [Fact]
    public async Task UserRequests_ShouldReturnForbiddenFor_AdminEndpoints()
    {
        await ExecuteUserRequestTests(_user, HttpStatusCode.Forbidden);
    }

    private async Task ExecuteUserRequestTests(
        HttpClient client,
        HttpStatusCode expectedStatusCode)
    {
        // Arrange
        var searchTerm = string.Empty;

        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client
                .GetAsync($"api/admin/hotel-reviews/search?query={searchTerm}"))
                    .StatusCode;

        statusCodes.AddRange(
            [searchStatusCode]);

        // Assert
        statusCodes.ForEach(statusCode =>
            statusCode.Should().Be(expectedStatusCode)
                .And.NotBe(HttpStatusCode.Unauthorized));
    }

    [Fact]
    public async Task Search_ShouldReturnOkFor_Admins()
    {
        // Arrange
        var searchTerm = _fixture.Create<string>();


        // Act
        var statusCode = (await _admin
            .GetAsync($"/api/admin/hotel-reviews/search?searchTerm={searchTerm}"))
                .StatusCode;

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
    }
}