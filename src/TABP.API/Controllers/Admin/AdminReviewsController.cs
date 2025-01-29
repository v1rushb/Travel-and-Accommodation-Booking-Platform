using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.HotelReview.Search;
using TABP.API.Extensions;
using Microsoft.AspNetCore.Authorization;
using TABP.Domain.Enums;
using TABP.Domain.Abstractions.Services.Review;

namespace TABP.API.Controllers.Admin;

[Authorize(Roles = nameof(RoleType.Admin))]
[ApiController]
[Route("api/admin/hotel-reviews")]
public class AdminReviewsControllers : ControllerBase
{
    private readonly IHotelReviewAdminService _hotelReviewAdminService;
    public AdminReviewsControllers(
        IHotelReviewAdminService hotelReviewAdminService)
    {
        _hotelReviewAdminService = hotelReviewAdminService;
    }

    [HttpGet("hotel-reviews/search")]
    public async Task<IActionResult> SearchHotelReviewsAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] AdminReviewSearchQuery query)
    {
        var reviews = await _hotelReviewAdminService
            .SearchAsync(
                query,
                pagination
            );

        var reviewCount = reviews.Count();

        Response.Headers
            .AddPaginationHeaders(
                reviewCount,
                pagination
            );

        return Ok(reviews);
    }
    
}