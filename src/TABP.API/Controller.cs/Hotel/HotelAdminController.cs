using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TABP.Abstractions.Services;
using TABP.API.Extensions;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.Hotels;
using TABP.Domain.Models.Pagination;

namespace TABP.API.Controllers;

// [Authorize] // add admin later.
[ApiController]
[Route("api/hotel/admin")]
public class HotelAdminController : ControllerBase
{
    private readonly IHotelService _hotelService;
    private readonly IMapper _mapper;
    private readonly IRoomService _roomService;
    private readonly IDiscountService _discountService;

    public HotelAdminController(
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
    public async Task<IActionResult> CreateHotelAsync([FromBody] HotelForCreationDTO newHotel)
    {
        // do some high level exception handling.

        var hotelId = await _hotelService.AddAsync(_mapper.Map<HotelDTO>(newHotel));

        return Created();
    }

    [HttpPatch("{hotelId:guid}")]
    public async Task<IActionResult> PatchHotelAsync(
        Guid hotelId,
        [FromBody] JsonPatchDocument<HotelForUpdateDTO> patchDoc)
    {
        var hotel = await _hotelService.GetByIdAsync(hotelId);
        var hotelToPartiallyUpdate = GetHotelForPartialUpdate(patchDoc, hotel);

        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(hotelToPartiallyUpdate, hotel);
        await _hotelService.UpdateAsync(hotel);

        return NoContent();
    }

    private HotelForUpdateDTO GetHotelForPartialUpdate(
        JsonPatchDocument<HotelForUpdateDTO> patchDoc,
        HotelDTO city)
    {
        var hotelToUpdate = _mapper.Map<HotelForUpdateDTO>(city);
        patchDoc.ApplyTo(hotelToUpdate, ModelState);

        return hotelToUpdate;
    }

    [HttpDelete("{hotelId:guid}")]
    public async Task<IActionResult> DeleteHotelAsync(Guid hotelId)
    {
        await _hotelService.DeleteAsync(hotelId);

        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchHotelsForAdminAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] HotelSearchQuery query)
    {
        var hotels = await _hotelService.SearchAdminAsync(query, pagination);
        var hotelCount = hotels.Count();

        Response.Headers.AddPaginationHeaders(hotelCount, pagination);

        return Ok(hotels);
    }

    [HttpGet("{hotelId:guid}")]
    public async Task<IActionResult> SearchHotelById(Guid hotelId)
    {
        var hotel = await _hotelService.GetByIdAsync(hotelId);
            _mapper.Map<HotelAdminWithoutIdResponseDTO>(hotel);
        return Ok(hotel);
    }
}