using TABP.Domain.Models.HotelReview.Search;
using TABP.Domain.Models.HotelReview.Search.Response;
using TABP.Domain.Models.HotelReview.Sort;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services.Review;

public interface IHotelReviewAdminService
{
    Task<IEnumerable<HotelReviewAdminResponseDTO>> SearchAsync(
        AdminReviewSearchQuery query,
        PaginationDTO pagination,
        ReviewSortQuery sortQuery);
}