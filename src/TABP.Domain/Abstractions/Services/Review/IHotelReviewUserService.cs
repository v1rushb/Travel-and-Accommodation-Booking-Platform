using TABP.Domain.Models.HotelReview.Search;
using TABP.Domain.Models.HotelReview.Search.Response;
using TABP.Domain.Models.HotelReview.Sort;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Entities;

namespace TABP.Domain.Abstractions.Services.Review;

/// <summary>
/// Provides operations for retrieving <see cref="HotelReview"/>details meant for end users.
/// This includes searching for rooms using filtering, sorting, and pagination.
/// </summary>
public interface IHotelReviewUserService
{
    /// <summary>
    /// Searches for<see cref="HotelReview"/>collection that satisfies your criteria.
    /// </summary>
    /// <param name="query">
    /// The search query containing filter parameters relevant to<see cref="HotelReview"/>.
    /// </param>
    /// <param name="pagination">
    /// The pagination parameters that control the result set size and page number.
    /// </param>
    /// <param name="sortQuery">
    /// The sorting parameters that specify the order of the results.
    /// </param>
    /// <returns>
    /// A collection of<see cref="HotelReviewUserResponseDTO"/>Representing <see cref="HotelReviews"/>.
    /// </returns>
    /// <exception cref="FluentValidation.ValidationException">
    /// Thrown if the pagination or sorting parameters are invalid.
    /// </exception>
    Task<IEnumerable<HotelReviewUserResponseDTO>> SearchAsync(
        ReviewSearchQuery query,
        PaginationDTO pagination,
        ReviewSortQuery sortQuery);
}