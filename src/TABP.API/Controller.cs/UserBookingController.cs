using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.RoomBooking;

namespace TABP.API.Controllers;

[ApiController]
[Route("api/users/{userId:guid}/bookings")]
public class UserBookingController : ControllerBase
{
    private readonly IRoomBookingService _roomBookingService;
    private readonly IMapper _mapper;

    public UserBookingController(
        IRoomBookingService roomBookingService,
        IMapper mapper)
    {
        _roomBookingService = roomBookingService;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] RoomBookingForCreationDTO newBooking)
    {
        // validations here.

        var bookingId = await _roomBookingService.AddAsync(_mapper.Map<RoomBookingDTO>(newBooking));
        
        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> GetForUserAsync(Guid userId)
    {
        var bookings = await _roomBookingService.GetByUserAsync(userId);
        return Ok(bookings);
    }

    [HttpPut("{bookingId:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid bookingId, [FromBody] RoomBookingForCreationDTO updatedBooking)
    {
        // validations here.

        await _roomBookingService.UpdateAsync(_mapper.Map<RoomBookingDTO>(updatedBooking));
        return NoContent();
    }

    [HttpDelete("{bookingId:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid bookingId)
    {
        await _roomBookingService.DeleteAsync(bookingId);
        return NoContent();
    }

    [HttpGet("{bookingId:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid userId, Guid bookingId)
    {
        var booking = await _roomBookingService.GetByIdAsync(bookingId);
        if(booking == null)
            return NotFound($"No booking {bookingId} found for the user {userId}");
        return Ok(booking);
    }
}