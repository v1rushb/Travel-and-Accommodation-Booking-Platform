using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.HotelVisit;
using TABP.Domain.Models.Pagination;
using TABP.API.Extensions;
using TABP.Domain.Abstractions.Services.Hotel;

namespace TABP.API.Controllers;

[Authorize]
[ApiController]
[Route("api/visits")]
public class VisitsController : ControllerBase
{
    private readonly IHotelVisitService _visitService;
    private readonly IHotelService _hotelService;
    private readonly IHotelUserService _hotelUserService;


    public VisitsController(
        IHotelVisitService historyService,
        IHotelService hotelService,
        IHotelUserService hotelUserService)
    {
        _visitService = historyService;
        _hotelService = hotelService;
        _hotelUserService = hotelUserService;
    }

    [HttpGet("top-hotels")]
    public async Task<IActionResult> GetTopFiveVisitedHotelsAsync(
        [FromQuery] VisitTimeOptionQuery query)
    {
        var hotels = await _visitService
            .GetTop5VisitedHotels(query);
        
        return Ok(hotels);
    }

    [HttpGet("hotels/history")]
    public async Task<IActionResult> GetHotelHistoryAsync(
        [FromQuery] VisitTimeOptionQuery query,
        [FromQuery] PaginationDTO pagination)
    {
        var hotels = await _hotelUserService
            .GetHotelHistoryAsync(
                pagination,
                query
            );

        var hotelCount = hotels.Count();

        Response.Headers
            .AddPaginationHeaders(
                hotelCount,
                pagination
            );

        return Ok(hotels);
    }
}