using System.Net;
using System.Net.Http.Json;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using TABP.Domain.Models.Discount;

namespace TABP.API.IntegrationTests;

public class DiscountAdminControllerTests : IClassFixture<HotelBookingFactory>, IAsyncLifetime
{
    private readonly HotelBookingFactory _factory;
    private HttpClient _guest;
    private HttpClient _user;
    private HttpClient _admin;
    private HttpClient _loggedOutAdmin;
    private readonly Fixture _fixture = new();

    public DiscountAdminControllerTests(HotelBookingFactory factory)
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
        var searchString = string.Empty;
        var newDiscount = new DiscountForCreationDTO();
        var discountId = new Guid();
        var discountPatch = new JsonPatchDocument<DiscountForUpdateDTO>();

        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client
                .GetAsync($"api/admin/discounts/search?query={searchString}"))
                    .StatusCode;

        var postStatusCode =
            (await client
                .PostAsJsonAsync("api/admin/discounts", newDiscount))
                    .StatusCode;

        var deleteStatusCode = (await client
            .DeleteAsync($"api/admin/discounts/{discountId}"))
                .StatusCode;

        var patchStatusCode =
            (await client
                .PatchAsJsonAsync($"api/admin/discounts/{discountId}", discountPatch))
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
        var newDiscount = new DiscountForCreationDTO();

        // Act
        var postStatusCode = (await _admin
            .PostAsJsonAsync("/api/admin/discounts", newDiscount))
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
        var searchString = string.Empty;
        var newDiscount = new DiscountForCreationDTO();
        var discountId = new Guid();
        var discountPatch = new JsonPatchDocument<DiscountForUpdateDTO>();

        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client
                .GetAsync($"api/admin/discounts/search?query={searchString}"))
                    .StatusCode;

        var postStatusCode =
            (await client
                .PostAsJsonAsync("api/admin/discounts", newDiscount))
                    .StatusCode;

        var deleteStatusCode = (await client
            .DeleteAsync($"api/admin/discounts/{discountId}"))
                .StatusCode;

        var patchStatusCode =
            (await client
                .PatchAsJsonAsync($"api/discounts/{discountId}", discountPatch))
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
        var searchString = string.Empty;
        var newDiscount = new DiscountForCreationDTO();
        var discountId = new Guid();
        var discountPatch = new JsonPatchDocument<DiscountForUpdateDTO>();

        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client
                .GetAsync($"api/admin/discounts/search?query={searchString}"))
                    .StatusCode;

        var postStatusCode =
            (await client
                .PostAsJsonAsync("api/admin/discounts", newDiscount))
                    .StatusCode;

        var deleteStatusCode = (await client
            .DeleteAsync($"api/admin/discounts/{discountId}"))
                .StatusCode;

        var patchStatusCode =
            (await client
                .PatchAsJsonAsync($"api/admin/discounts/{discountId}", discountPatch))
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
            .GetAsync($"/api/admin/discounts/search?searchTerm={searchTerm}"))
                .StatusCode;


        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
    }

}