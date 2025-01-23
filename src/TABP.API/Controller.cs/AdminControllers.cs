using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.HotelReview.Search;
using TABP.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using TABP.Domain.Models.Room.Search;
using TABP.Domain.Models.Booking.Search;
using TABP.Domain.Models.Cart.Search;

namespace TABP.API.Controllers;

[Authorize]
[ApiController]
[Route("api/admin")]
public class AdminControllers : ControllerBase
{
    private readonly IHotelReviewService _hotelReviewService;
    private readonly IRoomService _roomService;
    private readonly IRoomBookingService _roomBookingService;
    private readonly ICartService _cartService;

    public AdminControllers(
        IHotelReviewService hotelReviewService,
        IRoomService roomService,
        IRoomBookingService roomBookingService,
        ICartService cartService)
    {
        _hotelReviewService = hotelReviewService;
        _roomService = roomService;
        _roomBookingService = roomBookingService;
        _cartService = cartService;
    }

    [HttpGet("hotel-reviews/search")]
    public async Task<IActionResult> SearchHotelReviewsAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] AdminReviewSearchQuery query)
    {
        var reviews = await _hotelReviewService.SearchForAdminAsync(query, pagination);
        var reviewCount = reviews.Count();

        Response.Headers.AddPaginationHeaders(reviewCount, pagination);

        return Ok(reviews);
    }
    
    [HttpGet("hotel-rooms/search")] 
    public async Task<IActionResult> SearchRoomsAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] RoomSearchQuery query)
    {
        var rooms = await _roomService.SearchRoomsAsync(query, pagination);
        var roomCount = rooms.Count();

        Response.Headers.AddPaginationHeaders(roomCount, pagination);

        return Ok(rooms);
    }

    [HttpGet("bookings/search")]
    public async Task<IActionResult> SearchBookingsAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] AdminBookingSearchQuery query)
    {
        var bookings = await _roomBookingService.SearchAdminAsync(query, pagination);
        var bookingCount = bookings.Count();

        Response.Headers.AddPaginationHeaders(bookingCount, pagination);

        return Ok(bookings);
    }

    [HttpGet("carts/search")]
    public async Task<IActionResult> SearchCartsAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] CartSearchQuery query)
    {
        var carts = await _cartService.SearchCartsAsync(pagination, query);
        var cartCount = carts.Count();

        Response.Headers.AddPaginationHeaders(cartCount, pagination);

        return Ok(carts);
    }
}