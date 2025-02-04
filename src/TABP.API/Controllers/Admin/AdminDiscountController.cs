using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Models.Discount;
using TABP.Domain.Models.Discount.Search.Response;
using Microsoft.AspNetCore.JsonPatch;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Discount.Search;
using TABP.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using TABP.Domain.Enums;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Discount.Sort;

namespace TABP.API.Controllers.Admin;

[Authorize(Roles = nameof(RoleType.Admin))]
[ApiController]
[Route("/api/admin/discounts")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService _discountService;
    private readonly IMapper _mapper;

    public DiscountController(
        IDiscountService discountService,
        IMapper mapper)
    {
        _discountService = discountService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscountAsync([FromBody] DiscountForCreationDTO newDiscount)
    {
        var discount = _mapper.Map<DiscountDTO>(newDiscount);
        await _discountService.AddAsync(discount);

        return Created();
    }

    [HttpGet("{discountId:guid}")]
    public async Task<IActionResult> SearchByIdAsync(Guid discountId)
    {
        var discount = await _discountService.GetByIdAsync(discountId);

        var resultDiscount = _mapper.Map<DiscountForAdminWithoutIdResponseDTO>(discount);

        return Ok(resultDiscount);
    }

    [HttpPatch("{discountId:guid}")]
    public async Task<IActionResult> PatchDiscountAsync(
        Guid discountId,
        [FromBody] JsonPatchDocument<DiscountForUpdateDTO> patchDoc)
    {
        var discount = await _discountService.GetByIdAsync(discountId);
        var discountToPartiallyUpdate = GetDiscountForPartialUpdate(patchDoc, discount);

        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(discountToPartiallyUpdate, discount); 
        await _discountService.UpdateAsync(discount);

        return NoContent();
    }

    private DiscountForUpdateDTO GetDiscountForPartialUpdate(
        JsonPatchDocument<DiscountForUpdateDTO> patchDoc,
        DiscountDTO discount)
    {
        var discountToUpdate = _mapper.Map<DiscountForUpdateDTO>(discount);
        patchDoc.ApplyTo(discountToUpdate, ModelState);

        return discountToUpdate;
    }

    [HttpDelete("{discountId:guid}")]
    public async Task<IActionResult> DeleteDiscountAsync(Guid discountId)
    {
        await _discountService.DeleteAsync(discountId);
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchForAdminAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] DiscountSearchQuery query,
        [FromQuery] DiscountSortQuery sortQuery)
    {
        var discounts = await _discountService
            .SearchForAdminAsync(
                query,
                pagination,
                sortQuery
            );
            
        var discountsSize = discounts.Count();

        Response.Headers.AddPaginationHeaders(discountsSize, pagination);
        return Ok(discounts);
    }
}