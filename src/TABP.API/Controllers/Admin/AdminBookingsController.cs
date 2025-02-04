using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Enums;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Booking.Search;
using TABP.API.Extensions;
using TABP.Domain.Abstractions.Services.Booking;
using TABP.Domain.Models.RoomBooking;

namespace TABP.API.Controllers.Admin;

[Authorize(Roles = nameof(RoleType.Admin))]
[ApiController]
[Route("api/admin/bookings")]
public class AdminBookingsController : ControllerBase
{
    private readonly IRoomBookingAdminService _roomBookingAdminService;
    private readonly ILogger<AdminBookingsController> _logger;

    public AdminBookingsController(
        IRoomBookingAdminService roomBookingAdminService,
        ILogger<AdminBookingsController> logger)
    {
        _roomBookingAdminService = roomBookingAdminService;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchBookingsAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] AdminBookingSearchQuery query,
        [FromQuery] BookingSortQuery sortQuery)
    {
        var bookings = await _roomBookingAdminService
            .SearchAsync(
                query,
                pagination,
                sortQuery
            );
            
        var bookingCount = bookings.Count();

        _logger.LogInformation("Found {Count} bookings for the specified query", bookingCount);

        Response.Headers
            .AddPaginationHeaders(
                bookingCount,
                pagination
            );

        return Ok(bookings);
    }

    
}