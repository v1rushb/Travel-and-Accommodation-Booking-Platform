using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Hotels;

namespace TABP.API.Controllers;

internal class HotelController : ControllerBase
{
    private readonly IHotelService _hotelService;
    private readonly IMapper _mapper;

    public HotelController(
        IHotelService hotelService,
        IMapper mapper)
    {
        _hotelService = hotelService;
        _mapper = mapper;
    }
    public async Task<IActionResult> CreateAsync(HotelForCreationDTO newHotel)
    {
        // do some high level exception handling.

        await _hotelService.AddAsync(_mapper.Map<HotelDTO>(newHotel));

        return Created();
    }

    public async Task<IActionResult> DeleteAsync(Guid hotelId)
    {
        await _hotelService.DeleteAsync(hotelId);

        return NoContent();
    }

    public async 
}