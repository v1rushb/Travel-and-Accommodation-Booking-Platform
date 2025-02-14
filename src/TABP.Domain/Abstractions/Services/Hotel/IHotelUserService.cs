using TABP.Domain.Hotels;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.Hotel.Search;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.Hotel.Sort;
using TABP.Domain.Models.HotelVisit;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services;

/// <summary>
/// Defines operations for End-user hotel interactions.
/// </summary>
public interface IHotelUserService
{
    /// <summary>
    /// Searches for hotels based on the specified criteria.
    /// </summary>
    /// <param name="query">
    /// The hotel search query containing filtering options.
    /// </param>
    /// <param name="pagination">
    /// The pagination parameters to control the result set.
    /// </param>
    /// <param name="sortQuery">
    /// The sorting options to order the search results.
    /// </param>
    /// <returns>
    /// A collection of<see cref="HotelUserResponseDTO"/>Representing<see cref="Hotel"/>details matching critera.
    /// </returns>
    Task<IEnumerable<HotelUserResponseDTO>> SearchAsync(
        HotelSearchQuery query,
        PaginationDTO pagination,
        HotelSortQuery sortQuery);

    /// <summary>
    /// Retrieves a detailed<see cref="Hotel"/>page for the specified<see cref="Hotel"/>.
    /// </summary>
    /// <param name="hotelId">
    /// The unique<see cref="Guid"/>identifier of the<see cref="Hotel"/>.
    /// </param>
    /// <returns>
    /// <see cref="HotelPageResponseDTO"/>Representing the hotel page details.
    /// </returns>
    Task<HotelPageResponseDTO> GetHotelPageAsync(Guid hotelId);


    /// <summary>
    /// Retrieves a collection of<see cref="Hotel"/>featured for the current week.
    /// </summary>
    /// <returns>
    /// <see cref="FeaturedHotelDTO"/>Representing a collection of featured <see cref="Hotels"/> details.
    /// </returns>
    Task<IEnumerable<FeaturedHotelDTO>> GetWeeklyFeaturedHotelsAsync();
    

    /// <summary>
    /// Retrieves the <see cref="HotelVisit"/> history based on the specified criteria.
    /// </summary>
    /// <param name="pagination">
    /// The pagination parameters that control the result set.
    /// </param>
    /// <param name="query">
    /// The visit time option query to filter the hotel history.
    /// </param>
    /// <param name="userId">
    /// (Optional) The unique identifier of the user. If not provided, the current user's ID is used.
    /// </param>
    /// <returns>
    /// <see cref="HotelHistoryDTO"/>Representing collection of hotel history details.
    /// </returns>
    Task<IEnumerable<HotelHistoryDTO>> GetHotelHistoryAsync(
        PaginationDTO pagination,
        VisitTimeOptionQuery query,
        Guid? userId = null);

    /// <summary>
    /// Retrieves the current hotel hot deals.
    /// </summary>
    /// <returns>
    /// <see cref="HotelDealDTO"/>Representing a collection of hotel deals.
    /// </returns>
    Task<IEnumerable<HotelDealDTO>> GetDealsAsync();
}