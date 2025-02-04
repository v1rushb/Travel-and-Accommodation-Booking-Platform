using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Models.HotelReview;
using Microsoft.AspNetCore.JsonPatch;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.HotelReview.Search;
using TABP.API.Extensions;
using TABP.Domain.Abstractions.Services.Review;
using TABP.Domain.Models.HotelReview.Sort;

namespace TABP.API.Controllers;

[Authorize]
[ApiController]
[Route("api/hotel-reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IHotelReviewService _hotelReviewService;
    private readonly IHotelReviewUserService _hotelReviewUserService;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public ReviewsController(
        IHotelReviewService hotelReviewService,
        IHotelReviewUserService hotelReviewUserService,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _hotelReviewService = hotelReviewService;
        _hotelReviewUserService = hotelReviewUserService;
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
        [FromQuery] ReviewSearchQuery query,
        [FromQuery] ReviewSortQuery sortQuery)
    {
        var reviews = await _hotelReviewUserService
            .SearchAsync(
                query,
                pagination,
                sortQuery
            );

        var reviewsCount = reviews.Count();
        
        Response.Headers
            .AddPaginationHeaders(
                reviewsCount,
                pagination
            );

        return Ok(reviews);
    }
}