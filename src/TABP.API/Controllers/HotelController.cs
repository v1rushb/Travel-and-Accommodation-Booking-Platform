


using Microsoft.AspNetCore.Mvc;
using TABP.API.Extensions;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Hotel.Search;
using Microsoft.AspNetCore.Authorization;
using TABP.Domain.Enums;
using TABP.Domain.Models.Hotel.Sort;

namespace TABP.API.Controllers;

[Authorize(Roles = $"{nameof(RoleType.User)},{nameof(RoleType.Admin)}")]
[ApiController]
[Route("api/hotels")]
public class HotelController : ControllerBase
{
    private readonly IHotelUserService _hotelUserService;

    public HotelController(
        IHotelUserService hotelUserService)
    {
        _hotelUserService = hotelUserService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAndFilterHotelsAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] HotelSearchQuery query,
        [FromQuery] HotelSortQuery sortQuery)
    {
        var result = await _hotelUserService
            .SearchAsync(
                query,
                pagination,
                sortQuery
            );

        var hotelSize = result.Count();

        Response.Headers
            .AddPaginationHeaders(
                hotelSize,
                pagination
            );

        return Ok(result);
    }

    [HttpGet("{hotelId:guid}/page")]
    public async Task<IActionResult> GetHotelPageAsync(Guid hotelId)
    {
        var hotel = await _hotelUserService
            .GetHotelPageAsync(hotelId);

        return Ok(hotel);
    }

    [HttpGet("featured")]
    public async Task<IActionResult> GetWeeklyFeaturedHotelsAsync() // maybe paginate?
    {
        var hotels = await _hotelUserService
            .GetWeeklyFeaturedHotelsAsync();

        return Ok(hotels);
    }
}