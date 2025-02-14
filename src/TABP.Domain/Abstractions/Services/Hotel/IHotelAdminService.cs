using TABP.Domain.Models.Hotel.Search;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.Hotel.Sort;
using TABP.Domain.Models.HotelVisit;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Entities;

namespace TABP.Domain.Abstractions.Services.Hotel;

/// <summary>
/// Defines administrative operations for searching and managing hotels.
/// Provides a method to search for hotels using filtering, sorting, pagination, and visit time criteria.
/// </summary>
public interface IHotelAdminService
{
    /// <summary>
    /// Searches for hotels based on the specified criteria.
    /// </summary>
    /// <param name="query">
    /// The hotel search query containing filtering options.
    /// </param>
    /// <param name="pagination">
    /// The pagination parameters that control the result set size and page number.
    /// </param>
    /// <param name="sortQuery">
    /// The sorting parameters that determine the order of the results.
    /// </param>
    /// <param name="timeOptionQuery">
    /// The visit time option query used to filter hotels based on visit data.
    /// </param>
    /// <returns>
    /// A collection of<see cref="HotelAdminResponseDTO"/>representing<see cref="Hotel"/>.
    /// </returns>
    Task<IEnumerable<HotelAdminResponseDTO>> SearchAsync(
        HotelSearchQuery query,
        PaginationDTO pagination,
        HotelSortQuery sortQuery,
        VisitTimeOptionQuery timeOptionQuery);
}