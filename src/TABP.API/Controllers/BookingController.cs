using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.RoomBooking;
using TABP.Domain.Models.Booking.Search;
using TABP.API.Extensions;
using TABP.Domain.Models.CartItem;
using TABP.Domain.Enums;
using TABP.Domain.Abstractions.Services.Booking;
using TABP.Domain.Abstractions.Services.Cart;

namespace TABP.API.Controllers;

[Authorize(Roles = $"{nameof(RoleType.User)},{nameof(RoleType.Admin)}")]
[ApiController]
[Route("api/bookings")]
public class UserBookingController : ControllerBase
{
    private readonly IRoomBookingUserService _roomBookingUserService;
    private readonly IMapper _mapper;
    private readonly ICartService _cartService;

    public UserBookingController(
        IRoomBookingUserService roomBookingUserService,
        IMapper mapper,
        ICartService cartService)
    {
        _roomBookingUserService = roomBookingUserService;
        _mapper = mapper;
        _cartService = cartService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateBookingAsync([FromBody] RoomBookingForCreationDTO newBooking)
    {
        await _cartService.AddItemAsync(_mapper.Map<CartItemDTO>(newBooking));
        
        return Created();
    }

    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> DeleteBookingAsync(Guid Id)
    {
        await _cartService.DeleteItemAsync(Id);
        return Ok();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchUserBookingsAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] BookingSearchQuery query,
        [FromQuery] BookingSortQuery sortQuery)
    {
        var bookings = await _roomBookingUserService
            .SearchAsync(
                query,
                pagination,
                sortQuery
            );

        var bookingsCount = bookings.Count();

        Response.Headers
            .AddPaginationHeaders(
                bookingsCount,
                pagination
            );

        return Ok(bookings);
    }

    //do the update.
    // [HttpPatch("{bookingId:guid}")]
    // public async Task<IActionResult> PatchBookingAsync(
    //     Guid bookingId,
    //     [FromBody] JsonPatchDocument<RoomBookingForUpdateDTO> patchDoc)
    // {
    //     var booking = await _roomBookingService.GetByIdAsync(bookingId);
    //     var bookingToPartiallyUpdate = GetBookingForPartialUpdate(patchDoc, booking);

    //     if(!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }

    //     _mapper.Map(bookingToPartiallyUpdate, booking);
    //     await _roomBookingService.UpdateAsync(booking);

    //     return NoContent();
    // }

    // private RoomBookingForUpdateDTO GetBookingForPartialUpdate(
    //     JsonPatchDocument<RoomBookingForUpdateDTO> patchDoc,
    //     RoomBookingDTO booking)
    // {
    //     var bookingToUpdate = _mapper.Map<RoomBookingForUpdateDTO>(booking);
    //     patchDoc.ApplyTo(bookingToUpdate, ModelState);

    //     return bookingToUpdate;
    // }
}