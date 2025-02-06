using System.Net;
using System.Net.Http.Json;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using TABP.Domain.Models.Room;

namespace TABP.API.IntegrationTests;

public class RoomAdminControllerTests : IClassFixture<HotelBookingFactory>
{
    private readonly HttpClient _guest;
    private readonly HttpClient _user;
    private readonly HttpClient _admin;
    private readonly HttpClient _loggedOutAdmin;
    private readonly Fixture _fixture = new();

    public RoomAdminControllerTests(HotelBookingFactory factory)
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
        var newRoom = new RoomForCreationDTO();
        var roomId = new Guid();
        var roomPatch = new JsonPatchDocument<RoomForUpdateDTO>();

        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client
                .GetAsync($"api/admin/hotel-rooms/search?query={searchTerm}"))
                    .StatusCode;

        var postStatusCode =
            (await client
                .PostAsJsonAsync("api/admin/hotel-rooms", newRoom))
                    .StatusCode;

        var deleteStatusCode = (await client
            .DeleteAsync($"api/admin/hotel-rooms/{roomId}"))
                .StatusCode;

        var patchStatusCode =
            (await client
                .PatchAsJsonAsync($"api/admin/hotel-rooms/{roomId}", roomPatch))
                    .StatusCode;

        statusCodes.AddRange(
            [searchStatusCode, postStatusCode, deleteStatusCode, patchStatusCode]);

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
    public async Task AuthorizedPostRequest_ShouldReturnBadRequestFor_InvalidBody()
    {
        // Arrange
        var discountName = _fixture.Create<string>();
        var newRoom = new RoomForCreationDTO();

        // Act
        var postStatusCode = (await _admin
            .PostAsJsonAsync("/api/admin/hotel-rooms", newRoom))
                .StatusCode;

        // Assert
        postStatusCode.Should().Be(HttpStatusCode.BadRequest);
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
        var newRoom = new RoomForCreationDTO();
        var roomId = new Guid();
        var roomPatch = new JsonPatchDocument<RoomForUpdateDTO>();

        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client
                .GetAsync($"api/admin/hotel-rooms/search?query={searchTerm}"))
                    .StatusCode;

        var postStatusCode =
            (await client
                .PostAsJsonAsync("api/admin/hotel-rooms", newRoom))
                    .StatusCode;

        var deleteStatusCode = (await client
            .DeleteAsync($"api/admin/hotel-rooms/{roomId}"))
                .StatusCode;

        var patchStatusCode =
            (await client
                .PatchAsJsonAsync($"api/hotel-rooms/{roomId}", roomPatch))
                    .StatusCode;

        statusCodes.AddRange(
            [searchStatusCode, postStatusCode, deleteStatusCode, patchStatusCode]);

        // Assert
        statusCodes.ForEach(statusCode => statusCode
            .Should().Be(HttpStatusCode.Forbidden));
    }

    [Fact]
    private async Task UserRequests_ShouldReturnForbiddenFor_AdminEndpoints()
    {
        await ExecuteUserRequestTests(_user, HttpStatusCode.Forbidden);
    }

    private async Task ExecuteUserRequestTests(
        HttpClient client,
        HttpStatusCode expectedStatusCode)
    {
        // Arrange
        var searchTerm = string.Empty;
        var newRoom = new RoomForCreationDTO();
        var roomId = new Guid();
        var roomPatch = new JsonPatchDocument<RoomForUpdateDTO>();

        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client
                .GetAsync($"api/admin/hotel-rooms/search?query={searchTerm}"))
                    .StatusCode;

        var postStatusCode =
            (await client
                .PostAsJsonAsync("api/admin/hotel-rooms", newRoom))
                    .StatusCode;

        var deleteStatusCode = (await client
            .DeleteAsync($"api/admin/hotel-rooms/{roomId}"))
                .StatusCode;

        var patchStatusCode =
            (await client
                .PatchAsJsonAsync($"api/admin/hotel-rooms/{roomId}", roomPatch))
                    .StatusCode;

        statusCodes.AddRange(
            [searchStatusCode, postStatusCode, deleteStatusCode, patchStatusCode]);

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
            .GetAsync($"/api/admin/hotel-rooms/search?searchTerm={searchTerm}"))
                .StatusCode;


        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
    }

}