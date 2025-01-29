using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Enums;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Booking.Search;
using TABP.API.Extensions;
using TABP.Domain.Abstractions.Services.Booking;

namespace TABP.API.Controllers.Admin;

[Authorize(Roles = nameof(RoleType.Admin))]
[ApiController]
[Route("api/admin/bookings")]
public class AdminBookingsController : ControllerBase
{
    private readonly IRoomBookingAdminService _roomBookingAdminService;

    public AdminBookingsController(
        IRoomBookingAdminService roomBookingAdminService)
    {
        _roomBookingAdminService = roomBookingAdminService;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchBookingsAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] AdminBookingSearchQuery query)
    {
        var bookings = await _roomBookingAdminService
            .SearchAsync(
                query,
                pagination
            );
            
        var bookingCount = bookings.Count();

        Response.Headers
            .AddPaginationHeaders(
                bookingCount,
                pagination
            );

        return Ok(bookings);
    }

    
}