using TABP.Domain.Models.HotelReview.Search;
using TABP.Domain.Models.HotelReview.Search.Response;
using TABP.Domain.Models.HotelReview.Sort;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Entities;

namespace TABP.Domain.Abstractions.Services.Review;


/// <summary>
/// Defines methods for performing administrative operations on<see cref="HotelReview"/>.
/// </summary>
public interface IHotelReviewAdminService
{
    /// <summary>
    /// Searches for hotel reviews based on the specified criteria.
    /// </summary>
    /// <param name="query">
    /// The search query parameters, including filters specific to the review data.
    /// </param>
    /// <param name="pagination">
    /// The pagination parameters that control the page size and page number of the results.
    /// </param>
    /// <param name="sortQuery">
    /// The sorting parameters that determine the order of the results.
    /// </param>
    /// <returns>
    /// A collection of<see cref="HotelReviewAdminResponseDTO"/>Representing Reviews.
    /// </returns>
    /// <exception cref="FluentValidation.ValidationException">
    /// Thrown if the pagination or sorting parameters are invalid.
    /// </exception>
    Task<IEnumerable<HotelReviewAdminResponseDTO>> SearchAsync(
        AdminReviewSearchQuery query,
        PaginationDTO pagination,
        ReviewSortQuery sortQuery);
}