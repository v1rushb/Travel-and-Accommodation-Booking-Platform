using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.HotelReview;
using Microsoft.AspNetCore.JsonPatch;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.HotelReview.Search;
using TABP.API.Extensions;

namespace TABP.API.Controllers;

[Authorize]
[ApiController]
[Route("api/user/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IHotelReviewService _hotelReviewService;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public ReviewsController(
        IHotelReviewService hotelReviewService,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _hotelReviewService = hotelReviewService;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] HotelReviewForCreationDTO newReview)
    {
        var review = _mapper.Map<HotelReviewDTO>(newReview);

        await _hotelReviewService.AddAsync(review);

        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> GetForCurrentUserAsync()
    {
        var currentUserId = _currentUserService.GetUserId();
        var reviews = await _hotelReviewService.GetReviewsByUserAsync(currentUserId);

        return Ok(reviews);
    }

    // [HttpGet("{hotelId:guid}")]
    // public async Task<IActionResult> SearchByHotelAsync(Guid hotelId)
    // {
    //     var currentUserId = _currentUserService.GetUserId();
    //     var reviews = await _hotelReviewService.GetByUserAndHotelAsync(currentUserId, hotelId); 

    //     return Ok(reviews);
    // }

    [HttpPatch("{reviewId:guid}")]
    public async Task<IActionResult> PatchReviewAsync(
        Guid reviewId,
        [FromBody] JsonPatchDocument<HotelReviewForUpdateDTO> patchDoc)
    {
        var review = await _hotelReviewService.GetByIdAsync(reviewId);
        var reviewToPartiallyUpdate = GetReviewForPartialUpdate(patchDoc, review);

        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(reviewToPartiallyUpdate, review);
        await _hotelReviewService.UpdateAsync(review);

        return NoContent();
    }

    private HotelReviewForUpdateDTO GetReviewForPartialUpdate(
        JsonPatchDocument<HotelReviewForUpdateDTO> patchDoc,
        HotelReviewDTO review)
    {
        var reviewToUpdate = _mapper.Map<HotelReviewForUpdateDTO>(review);
        patchDoc.ApplyTo(reviewToUpdate, ModelState);

        return reviewToUpdate;
    }

    [HttpDelete("{reviewId:guid}")]
    public async Task<IActionResult> DeleteReviewAsync(Guid reviewId)
    {
        await _hotelReviewService.DeleteAsync(reviewId);

        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchReviewsAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] ReviewSearchQuery query) 
    {
        var reviews = await _hotelReviewService.SearchReviewsAsync(query, pagination);
        var reviewsCount = reviews.Count();
        
        Response.Headers.AddPaginationHeaders(reviewsCount, pagination);

        return Ok(reviews);
    }
}