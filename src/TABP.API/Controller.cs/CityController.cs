using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.City;

namespace TABP.API.Controllers;

[ApiController]
[Route("api/city")]
public class CityController : ControllerBase
{
    private readonly ICityService _cityService;
    private readonly IMapper _mapper;

    public CityController(
        ICityService cityService,
        IMapper mapper)
    {
        _cityService = cityService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CityForCreationDTO newCity)
    {
        var Id = await _cityService.AddAsync(_mapper.Map<CityDTO>(newCity));

        // return CreatedAtAction(nameof(GetByIdAsync), new { Id });
        return Created();
    }

    [HttpGet("{Id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid Id)
    {
        var city = await _cityService.GetByIdAsync(Id);
        return Ok(city);
    }

    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid Id)
    {
        await _cityService.DeleteAsync(Id);

        return NoContent();
    }

    [HttpPut("{Id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid Id, [FromBody] CityForCreationDTO newCity)
    {
        await _cityService.UpdateAsync(_mapper.Map<CityDTO>(newCity));
        return NoContent();
    }
}