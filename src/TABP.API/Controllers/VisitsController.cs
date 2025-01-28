using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.HotelVisit;
using TABP.Domain.Models.Pagination;
using TABP.API.Extensions;

namespace TABP.API.Controllers;

[Authorize]
[ApiController]
[Route("api/history")]
public class VisitsController : ControllerBase
{
    private readonly IHotelVisitService _visitService;
    private readonly IHotelService _hotelService;


    public VisitsController(
        IHotelVisitService historyService,
        IHotelService hotelService)
    {
        _visitService = historyService;
        _hotelService = hotelService;
    }

    [HttpGet("hotels/most-visited")]
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
        var hotels = await _hotelService
            .GetHotelHistoryAsync(pagination, query);

        var hotelCount = hotels.Count();

        Response.Headers.AddPaginationHeaders(hotelCount, pagination);

        return Ok(hotels);
    }
}