using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.RoomBooking;
using TABP.Domain.Models.Booking.Search;
using TABP.API.Extensions;
using Microsoft.AspNetCore.JsonPatch;
using TABP.Domain.Models.Booking;

namespace TABP.API.Controllers;

[Authorize]
[ApiController]
[Route("api/user/bookings")]
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
        await _roomBookingService.AddAsync(_mapper.Map<RoomBookingDTO>(newBooking));
        
        return Created();
    }

    // [HttpGet]
    // public async Task<IActionResult> GetBookingsForCurrentUserAsync()
    // {
    //     // var userId = new Guid(HttpContext.User); 
    //     // _logger.LogInformation("User {UserId} requested bookings for himself", userId);
    //     // var bookings = await _roomBookingService.GetByUserAsync(userId);

    //     // return Ok(bookings);
    //     // _logger.LogCritical("User {UserId} requested bookings for himself", HttpContext.User.);

    //     // var claims = HttpContext.User.Claims.ToList();
    //     // claims.ForEach(claim => _logger.LogCritical("Claim: {Claim}", claim.Type));
    //     // var userId = new Guid(claims.First(claim => claim.Type == "name").Value);
    //     // _logger.LogInformation("User {UserId} requested bookings for himself", userId);
    //     // _logger.LogCritical("User {UserId} requested bookings for himself", HttpContext.User.Identity.Name);
    //     // var role = HttpContext.User.IsInRole("");
    //     // _logger.LogCritical("User {UserId} requested bookings for himself", role);

    //     // var list = HttpContext.User.Identities.ToList();
    //     // list.ForEach(identity => _logger.LogCritical("Identity: {Identity}", identity.Name));

    //     var bookings = await _roomBookingService.GetByUserAsync();

    //     return Ok(bookings);
    // }

    [HttpGet("search")]
    public async Task<IActionResult> SearchUserBookingsAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] BookingSearchQuery query)
    {
        var bookings = await _roomBookingService.SearchUserBookingsAsync(query, pagination);
        var bookingsCount = bookings.Count();

        Response.Headers.AddPaginationHeaders(bookingsCount, pagination);

        return Ok(bookings);
    }

    [HttpPatch("{bookingId:guid}")]
    public async Task<IActionResult> PatchBookingAsync(
        Guid bookingId,
        [FromBody] JsonPatchDocument<RoomBookingForUpdateDTO> patchDoc)
    {
        var booking = await _roomBookingService.GetByIdAsync(bookingId);
        var bookingToPartiallyUpdate = GetBookingForPartialUpdate(patchDoc, booking);

        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(bookingToPartiallyUpdate, booking);
        await _roomBookingService.UpdateAsync(booking);

        return NoContent();
    }

    private RoomBookingForUpdateDTO GetBookingForPartialUpdate(
        JsonPatchDocument<RoomBookingForUpdateDTO> patchDoc,
        RoomBookingDTO booking)
    {
        var bookingToUpdate = _mapper.Map<RoomBookingForUpdateDTO>(booking);
        patchDoc.ApplyTo(bookingToUpdate, ModelState);

        return bookingToUpdate;
    }

    [HttpDelete("{bookingId:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid bookingId)
    {
        await _roomBookingService.DeleteAsync(bookingId);

        return NoContent();
    }

    
}