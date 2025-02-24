using System.Net;
using System.Net.Http.Json;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using TABP.Domain.Models.HotelReview;

namespace TABP.API.IntegrationTests;

public class ReviewControllerTests : IClassFixture<HotelBookingFactory>, IAsyncLifetime
{
    private readonly HotelBookingFactory _factory;
    private HttpClient _guest;
    private HttpClient _user;
    private HttpClient _admin;
    private HttpClient _loggedOutAdmin;
    private HttpClient _loggedOutUser;
    private readonly Fixture _fixture = new();

    public ReviewControllerTests(HotelBookingFactory factory)
    {
        _factory = factory;
    }

    public async Task InitializeAsync()
    {
        _guest = await _factory.GetGuestClient();
        _user = await _factory.GetAuthenticatedUserClientAsync();
        _admin = await _factory.GetAuthenticatedAdminClientAsync();
        _loggedOutAdmin = await _factory.GetLoggedOutAdminClientAsync();
        _loggedOutUser = await _factory.GetLoggedOutUserClientAsync();
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
        var newReview = new HotelReviewForCreationDTO();
        var reviewId = new Guid();
        var roomPatch = new JsonPatchDocument<HotelReviewForUpdateDTO>();

        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client
                .GetAsync($"api/hotel-reviews/search?query={searchTerm}"))
                    .StatusCode;

        var postStatusCode =
            (await client
                .PostAsJsonAsync("api/hotel-reviews", newReview))
                    .StatusCode;

        var deleteStatusCode = (await client
            .DeleteAsync($"api/hotel-reviews/{reviewId}"))
                .StatusCode;

        var patchStatusCode =
            (await client
                .PatchAsJsonAsync($"api/hotel-reviews/{reviewId}", roomPatch))
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
        var newReview = new HotelReviewForCreationDTO();

        // Act
        var postStatusCode = (await _user
            .PostAsJsonAsync("/api/hotel-reviews", newReview))
                .StatusCode;

        // Assert
        postStatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Requests_ReturnForbiddenFor_LoggedOutAdmins()
    {
        await ExecuteLoggedOutRequestTests(_loggedOutAdmin);
    }

    [Fact]
    public async Task Requests_ReturnForbiddenFor_LoggedOutUser()
    {
        await ExecuteLoggedOutRequestTests(_loggedOutUser);
    }

    private async Task ExecuteLoggedOutRequestTests(
        HttpClient client)
    {
        // Arrange
        var searchTerm = string.Empty;
        var newReview = new HotelReviewForCreationDTO();
        var reviewId = new Guid();
        var roomPatch = new JsonPatchDocument<HotelReviewForUpdateDTO>();

        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client
                .GetAsync($"api/hotel-reviews/search?query={searchTerm}"))
                    .StatusCode;

        var postStatusCode =
            (await client
                .PostAsJsonAsync("api/hotel-reviews", newReview))
                    .StatusCode;

        var deleteStatusCode = (await client
            .DeleteAsync($"api/hotel-reviews/{reviewId}"))
                .StatusCode;

        var patchStatusCode =
            (await client
                .PatchAsJsonAsync($"api/hotel-reviews/{reviewId}", roomPatch))
                    .StatusCode;

        statusCodes.AddRange(
            [searchStatusCode, postStatusCode, deleteStatusCode, patchStatusCode]);

        // Assert
        statusCodes.ForEach(statusCode => statusCode
            .Should().Be(HttpStatusCode.Forbidden));
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

    [Fact]
    public async Task Search_ShouldReturnOkFor_Admins()
    {
        // Arrange
        var searchTerm = _fixture.Create<string>();


        // Act
        var statusCode = (await _admin
            .GetAsync($"/api/hotel-reviews/search?searchTerm={searchTerm}"))
                .StatusCode;


        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
    }
}