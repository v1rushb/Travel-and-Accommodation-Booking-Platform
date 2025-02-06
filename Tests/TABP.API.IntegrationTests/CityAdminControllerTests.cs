using System.Net;
using System.Net.Http.Json;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.JsonPatch;
using TABP.Domain.Models.City;

namespace TABP.API.IntegrationTests;

public class CityAdminControllerTests : IClassFixture<HotelBookingFactory>
{
    private readonly HttpClient _guest;
    private readonly HttpClient _user;
    private readonly HttpClient _admin;
    private readonly HttpClient _loggedOutAdmin;
    private readonly Fixture _fixture = new();

    public CityAdminControllerTests(HotelBookingFactory factory)
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
        await ExecuteUserRequestTests(_guest, HttpStatusCode.Unauthorized);
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
        var newCity = new CityForCreationDTO();
        var cityId = new Guid();
        var cityPatch = new JsonPatchDocument<CityForUpdateDTO>();
        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client.GetAsync($"api/admin/cities/search?query={searchString}"))
            .StatusCode;

        var postStatusCode =
            (await client.PostAsJsonAsync("api/admin/cities", newCity))
            .StatusCode;

        var deleteStatusCode = (await client.DeleteAsync($"api/admin/cities/{cityId}"))
            .StatusCode;

        var patchStatusCode =
            (await client.PatchAsJsonAsync($"api/admin/cities/{cityId}", cityPatch))
            .StatusCode;

        statusCodes.AddRange(
            [searchStatusCode, postStatusCode, deleteStatusCode, patchStatusCode]);

        // Assert
        if(isGuest) {
            statusCodes.ForEach(statusCode => statusCode.Should().Be(expectedStatusCode));
        } else {
            statusCodes.ForEach(statusCode =>
                statusCode.Should().NotBe(HttpStatusCode.Unauthorized)
                    .And.NotBe(HttpStatusCode.Forbidden));
        }
    }

    [Fact]
    public async Task Requests_ShouldBeAuthorizedFor_Users()
    {
        await ExecuteUserRequestTests(_user, isGuest: false);
    }

    [Fact]
    public async Task UserEndpoints_ShouldBeAuthorized_ForAdmins()
    {
        await ExecuteUserRequestTests(_admin, isGuest: false);
    }

    private async Task ExecuteUserRequestTests(
        HttpClient client,
        HttpStatusCode? expectedStatusCode = null,
        bool isGuest = true)
    {
        // Arrange
        var searchString = string.Empty;
        var newCity = new CityForCreationDTO();
        var cityId = new Guid();
        var cityPatch = new JsonPatchDocument<CityForUpdateDTO>();
        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client.GetAsync($"api/cities/search?query={searchString}"))
            .StatusCode;

        statusCodes.AddRange(
            [searchStatusCode]);

        // Assert
        if(isGuest) {
            statusCodes.ForEach(statusCode => statusCode.Should().Be(expectedStatusCode));
        } else {
            statusCodes.ForEach(statusCode =>
                statusCode.Should().NotBe(HttpStatusCode.Unauthorized)
                    .And.NotBe(HttpStatusCode.Forbidden));
        }
    }

    [Fact]
    public async Task SearchCities_ReturnsOkFor_Admin()
    {
        // Act
        var responseCode = (await _admin.GetAsync("api/admin/cities/search")).StatusCode;

        // Assert
        responseCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Requests_ShouldReturnForbiddenFor_LoggedOutClients()
    {
        await ExecuteLoggedOutRequestTests(_loggedOutAdmin, HttpStatusCode.Forbidden);         
    }


    private async Task ExecuteLoggedOutRequestTests(
        HttpClient client,
        HttpStatusCode expectedStatusCode)
    {
        // Arrange
        var searchString = string.Empty;
        var newCity = new CityForCreationDTO{ Name = "Hiiiii"};
        var cityId = new Guid();
        var cityPatch = new JsonPatchDocument<CityForUpdateDTO>();
        var statusCodes = new List<HttpStatusCode>();

        // Act
        var searchStatusCode =
            (await client.GetAsync($"api/admin/cities/search?query={searchString}"))
            .StatusCode;

        var postStatusCode =
            (await client.PostAsJsonAsync("api/admin/cities", newCity))
            .StatusCode;
        var deleteStatusCode = (await client.DeleteAsync($"api/admin/cities/{cityId}"))
            .StatusCode;
        var patchStatusCode =
            (await client.PatchAsJsonAsync($"api/admin/cities/{cityId}", cityPatch))
            .StatusCode;
        statusCodes.AddRange(
            [searchStatusCode, postStatusCode, deleteStatusCode, patchStatusCode]);

        statusCodes.ForEach(statusCode => statusCode.Should().Be(expectedStatusCode));
    }

    [Fact]
    public async Task AuthorizedPostCities_ReturnsBadRequestFor_InvalidBody()
    {
        // Arrange
        var cityName = _fixture.Create<string>();
        var newCity = new CityForCreationDTO
        {
            Name= cityName
        };
        var statusCodes = new List<HttpStatusCode>();

        var adminStatusCode = (await _admin.PostAsJsonAsync("api/admin/cities", newCity)).StatusCode;

        statusCodes.Add(adminStatusCode);

        statusCodes.ForEach(statusCode => statusCode
            .Should().Be(HttpStatusCode.BadRequest));
    }

    [Fact]
    public async Task AuthorizedPostCities_ReturnNoContentFor_ValidBody()
    {
        // Arrange
        var cityName = _fixture.Create<string>();
        var countryName = _fixture.Create<string>();
        var newCity = new CityForCreationDTO
        {
            Name = cityName,
            CountryName = countryName
        };
        var statusCodes = new List<HttpStatusCode>();

        var adminStatusCode = (await _admin.PostAsJsonAsync("api/admin/cities", newCity)).StatusCode;

        statusCodes.Add(adminStatusCode);

        statusCodes.ForEach(statusCode => statusCode
            .Should().Be(HttpStatusCode.NoContent));
    }


    [Fact]
    public async Task SearchCities_ReturnsOkFor_User()
    {
        // Arrange
        var searchString = _fixture.Create<string>();

        // Act
        var responseCode =
            (await _user.GetAsync($"api/cities/search?query={searchString}"))
            .StatusCode;

        // Assert
        responseCode.Should().Be(HttpStatusCode.OK);
    }
}