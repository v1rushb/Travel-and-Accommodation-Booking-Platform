using TABP.Domain.Models.HotelReview.Search;
using TABP.Domain.Models.HotelReview.Search.Response;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services.Review;

public interface IHotelReviewUserService
{
    Task<IEnumerable<HotelReviewUserResponseDTO>> SearchAsync(ReviewSearchQuery query, PaginationDTO pagination);
}