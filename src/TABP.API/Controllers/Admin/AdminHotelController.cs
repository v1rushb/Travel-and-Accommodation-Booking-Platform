using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Extensions;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.Hotels;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Hotel.Search;
using Microsoft.AspNetCore.Authorization;
using TABP.Domain.Enums;
using TABP.Domain.Abstractions.Services.Hotel;
using TABP.Domain.Models.Hotel.Sort;

namespace TABP.API.Controllers.Admin;

[Authorize(Roles = nameof(RoleType.Admin))]
[ApiController]
[Route("api/admin/hotels")]
public class HotelAdminController : ControllerBase
{
    private readonly IHotelService _hotelService;
    private readonly IHotelAdminService _hotelAdminService;
    private readonly IMapper _mapper;

    public HotelAdminController(
        IHotelService hotelService,
        IHotelAdminService hotelAdminService,
        IMapper mapper)
    {
        _hotelService = hotelService;
        _hotelAdminService = hotelAdminService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateHotelAsync([FromBody] HotelForCreationDTO newHotel)
    {
        // do some high level exception handling.

        await _hotelService.AddAsync(_mapper.Map<HotelDTO>(newHotel));

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
        [FromQuery] HotelSearchQuery query,
        [FromQuery] HotelSortQuery sortQuery)
    {
        var hotels = await _hotelAdminService
            .SearchAsync(
                query,
                pagination,
                sortQuery
            );

        var hotelCount = hotels.Count();

        Response.Headers
            .AddPaginationHeaders(
                hotelCount,
                pagination
            );

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