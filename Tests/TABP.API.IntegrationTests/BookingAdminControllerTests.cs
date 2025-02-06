using TABP.API.IntegrationTests;
using System.Net;
using AutoFixture;
using FluentAssertions;


namespace TABP.API.IT;

public class BookingAdminControllerTests : IClassFixture<HotelBookingFactory>
{
    private readonly HttpClient _guest;
    private readonly HttpClient _user;
    private readonly HttpClient _admin;
    private readonly HttpClient _loggedOutAdmin;
    private readonly Fixture _fixture = new();

    public BookingAdminControllerTests(HotelBookingFactory factory)
    {
        _guest = factory.GetGuestClient();
        _user = factory.GetAuthenticatedUserClientAsync().Result;
        _admin = factory.GetAuthenticatedAdminClientAsync().Result;
        _loggedOutAdmin = factory.GetLoggedOutAdminClientAsync().Result;
    }

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
                .GetAsync($"api/admin/bookings/search?searchTerm={searchTerm}"))
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
                .GetAsync($"api/admin/bookings/search?query={searchTerm}"))
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
                .GetAsync($"api/admin/bookings/search?query={searchTerm}"))
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
            .GetAsync($"/api/admin/bookings/search?searchTerm={searchTerm}"))
                .StatusCode;

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
    }
}