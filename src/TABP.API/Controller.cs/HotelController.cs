using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TABP.Abstractions.Services;
using TABP.API.Extensions;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Discount;
using TABP.Domain.Models.Hotels;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Room;
using TABP.Domain.Models.Hotel.Search;

namespace TABP.API.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelController : ControllerBase
{
    private readonly IHotelService _hotelService;
    private readonly IMapper _mapper;
    private readonly IRoomService _roomService;
    private readonly IDiscountService _discountService;

    public HotelController(
        IHotelService hotelService,
        IMapper mapper,
        IRoomService roomService,
        IDiscountService discountService)
    {
        _hotelService = hotelService;
        _mapper = mapper;
        _roomService = roomService;
        _discountService = discountService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] HotelForCreationDTO newHotel)
    {
        // do some high level exception handling.

        var hotelId = await _hotelService.AddAsync(_mapper.Map<HotelDTO>(newHotel));

        return Created();
    }

    [HttpGet("{Id:guid}")]
    public async Task<IActionResult> GetHotelAsync(Guid hotelId)
    {
        var hotel = await _hotelService.GetByIdAsync(hotelId);
        return Ok(hotel);
    }

    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid hotelId)
    {
        await _hotelService.DeleteAsync(hotelId);

        return NoContent();
    }

    [HttpPut("{Id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid hotelId, [FromBody] HotelForCreationDTO newHotel)
    {
        await _hotelService.UpdateAsync(_mapper.Map<HotelDTO>(newHotel));
        return NoContent();
    }

    [HttpPost("{hotelId:guid}/room")]
    public async Task<IActionResult> CreateForHotelAsync(Guid hotelId, [FromBody] RoomForCreationDTO newRoom)
    {
        newRoom.HotelId = hotelId;
        var roomId = await _roomService.AddAsync(_mapper.Map<RoomDTO>(newRoom));

        return Created();
    }

    [HttpGet("{hotelId:guid}/room/{roomId:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid hotelId, Guid roomId)
    {
        var room = await _roomService.GetByIdAsync(roomId);
        //validations plssss.
        if(room == null)
            return NotFound("No room seems to be found for the given hotel.");

        return Ok(room);
    }

    [HttpPut("{hotelId:guid}/room/{roomId:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid hotelId, Guid roomId, [FromBody] RoomForCreationDTO updatedRoom)
    {
        if(updatedRoom.HotelId != hotelId)
        {
            return BadRequest("The room belongs to a different hotel.");
        }

        await _roomService.UpdateAsync(_mapper.Map<RoomDTO>(updatedRoom));
        return NoContent();
    }

    [HttpDelete("{hotelId:guid}/room/{roomId:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid hotelId, Guid roomId)
    {
        await _roomService.DeleteAsync(roomId);
        return NoContent();
    }
    //////////////////////////////////////////////////////////////////////
    // Discount Endpoints.

    [HttpPost("{hotelId:guid}/discounts")]
    public async Task<IActionResult> CreateAsync(Guid hotelId, [FromBody] DiscountForCreationDTO newDiscount)
    {
        newDiscount.HotelId = hotelId;
        var discountId = await _discountService.AddAsync(_mapper.Map<DiscountDTO>(newDiscount));

        return Created();
    }

    [HttpGet("{hotelId:guid}/discounts/{discountId:guid}")]
    public async Task<IActionResult> GetDiscountByIdAsync(Guid hotelId, Guid discountId)
    {
        var discount = await _discountService.GetByIdAsync(discountId);
        //validations plssss.
        if(discount == null)
            return NotFound("No discount seems to be found for the given hotel.");

        return Ok(discount);
    }

    [HttpGet("{hotelId:guid}/discounts")]
    public async Task<IActionResult> GetDiscountsByHotelAsync(Guid hotelId)
    {
        var discounts = await _discountService.GetByHotelAsync(hotelId);
        return Ok(discounts);
    }

    [HttpPut("{hotelId:guid}/discounts/{discountId:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid hotelId, Guid discountId, [FromBody] DiscountForCreationDTO updatedDiscount)
    {
        if(updatedDiscount.HotelId != hotelId)
        {
            return BadRequest("The discount belongs to a different hotel.");
        }

        await _discountService.UpdateAsync(_mapper.Map<DiscountDTO>(updatedDiscount));
        return NoContent();
    }

    [HttpDelete("{hotelId:guid}/discounts/{discountId:guid}")]
    public async Task<IActionResult> DeleteDiscountAsync(Guid hotelId, Guid discountId)
    {
        await _discountService.DeleteAsync(discountId);
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAndFilterHotelsAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] HotelSearchQuery query)
    {
        var result = await _hotelService.SearchAsync(query, pagination); // maybe SearchAndFilterAsync(query)?
        var hotelSize = result.Count();

        Response.Headers.AddPaginationHeaders(hotelSize, pagination);

        return Ok(result);
    }
}