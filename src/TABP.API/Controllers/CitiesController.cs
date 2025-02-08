using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Enums;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.City.Search;
using TABP.API.Extensions;
using TABP.Domain.Abstractions.Services.City;
using TABP.Domain.Models.City.Sort;

namespace TABP.API.Controllers;

[Authorize(Roles = $"{nameof(RoleType.User)},{nameof(RoleType.Admin)}")]
[ApiController]
[Route("api/cities")]
public class CitiesController : ControllerBase
{
    private readonly ICityUserService _cityUserService;

    public CitiesController(
        ICityUserService cityUserService)
    {
        _cityUserService = cityUserService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAndFilterCitiesAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] CitySearchQuery query,
        [FromQuery] CitySortQuery sortQuery)
    {
        var result = await _cityUserService
            .SearchAsync(
                query,
                pagination,
                sortQuery
            );

        var citySize = result.Count();

        Response.Headers
            .AddPaginationHeaders(
                citySize,
                pagination
            );

        return Ok(result);
    }
}