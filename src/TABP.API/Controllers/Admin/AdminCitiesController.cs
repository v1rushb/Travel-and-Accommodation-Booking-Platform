using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Extensions;
using TABP.Domain.Models.City;
using TABP.Domain.Models.City.Search;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Enums;
using TABP.Domain.Abstractions.Services.City;
using TABP.Domain.Models.City.Sort;

namespace TABP.API.Controllers.Admin;

[Authorize(Roles = nameof(RoleType.Admin))]
[ApiController]
[Route("api/admin/cities")]
public class CityAdminController : ControllerBase
{
    private readonly ICityService _cityService;
    private readonly ICityAdminService _cityAdminService;
    private readonly IMapper _mapper;

    public CityAdminController(
        ICityService cityService,
        ICityAdminService cityAdminService,
        IMapper mapper)
    {
        _cityService = cityService;
        _cityAdminService = cityAdminService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CityForCreationDTO newCity)
    {
        await _cityService
            .AddAsync(_mapper.Map<CityDTO>(newCity));

        return Created();
    }

    [HttpGet("{Id:guid}")]
    public async Task<IActionResult> SearchByIdAsync(Guid Id)
    {
        var city = _mapper
            .Map<CitySearchResponseDTO>(await _cityService.GetByIdAsync(Id));  

        return Ok(city);
    }

    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid Id)
    {
        await _cityService.DeleteAsync(Id);

        return NoContent();
    }

    [HttpPatch("{cityId:guid}")]
    public async Task<IActionResult> PatchCityAsync(
        Guid cityId, JsonPatchDocument<CityForUpdateDTO> patchDoc) // exception handling later
    {
        var cityToUpdate = await _cityService
            .GetByIdAsync(cityId);

        var cityToPartiallyUpdate = 
            GetCityForPartialUpdate(
                patchDoc,
                cityToUpdate
            );

        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
    
        _mapper
            .Map(
                cityToPartiallyUpdate,
                cityToUpdate
            );

        await _cityService
            .UpdateAsync(
                cityToUpdate
            );

        return NoContent();
    }

    private CityForUpdateDTO GetCityForPartialUpdate(
        JsonPatchDocument<CityForUpdateDTO> patchDoc,
        CityDTO city)
    {
        var cityToUpdate = _mapper
            .Map<CityForUpdateDTO>(city);

        patchDoc
            .ApplyTo(
                cityToUpdate,
                ModelState
            );

        return cityToUpdate;
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchForAdminAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] CitySearchQuery query,
        [FromQuery] CitySortQuery sortQuery)
    {
        var result = await _cityAdminService
            .SearchAsync(
                query,
                pagination,
                sortQuery
            );

        var citySize = result.Count();

        Response.Headers
            .AddPaginationHeaders(
                citySize,
                pagination
            );

        return Ok(result);
    }
}