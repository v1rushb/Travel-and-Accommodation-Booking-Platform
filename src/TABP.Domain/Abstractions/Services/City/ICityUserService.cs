using TABP.Domain.Models.City.Search;
using TABP.Domain.Models.City.Sort;
using TABP.Domain.Models.HotelVisit;
using TABP.Domain.Models.Pagination;
using TABP.Models.City;

namespace TABP.Domain.Abstractions.Services.City;


/// <summary>
/// Defines operations for end-user interactions with city data.
/// This interface provides methods for searching cities from a user perspective and
/// retrieving trending cities based on visit data. It is tailored for user-facing applications
/// where city information is presented for browsing and discovery.
/// </summary>
public interface ICityUserService
{
    /// <summary>
    /// Searches for cities based on the specified criteria, intended for end-users.
    /// This method allows users to search cities using various filters, pagination, and sorting options.
    /// It retrieves city information suitable for display in user interfaces, focusing on data relevant to users.
    /// </summary>
    /// <param name="query">
    /// The city search query containing filtering options provided by the user.
    /// This includes criteria such as city name, geographical area, or other user-relevant filters.
    /// </param>
    /// <param name="pagination">
    /// The pagination parameters that control the size of the result set and the current page number.
    /// Enables efficient handling of search results by dividing them into manageable pages for user browsing.
    /// </param>
    /// <param name="sortQuery">
    /// The sorting parameters that define the order in which search results are presented to the user.
    /// Users can sort cities based on criteria like popularity, name, or other relevant attributes, in ascending or descending order.
    /// </param>
    /// <returns>
    /// A collection of <see cref="CitySearchResponseDTO"/> representing cities that match the user's search criteria.
    /// Each <see cref="CitySearchResponseDTO"/> object contains city details optimized for user display, such as name, description, and relevant user-facing attributes.
    /// </returns>
    Task<IEnumerable<CitySearchResponseDTO>> SearchAsync(
        CitySearchQuery query,
        PaginationDTO pagination,
        CitySortQuery sortQuery);


    /// <summary>
    /// Retrieves a list of trending cities based on visit data within a specified time frame.
    /// This method identifies cities that are currently popular among users, based on hotel visit metrics.
    /// It is used to provide users with recommendations or highlights of popular destinations.
    /// </summary>
    /// <param name="timeQuery">
    /// The time option query that specifies the period for which visit data should be considered.
    /// This allows for retrieving trending cities over different time frames, such as weekly, monthly, or yearly trends.
    /// </param>
    /// <param name="pagination">
    /// The pagination parameters to control the number of trending cities returned and to manage result pages.
    /// Ensures that the list of trending cities is presented in a paginated format for better user experience, especially when dealing with potentially large result sets.
    /// </param>
    /// <returns>
    /// A collection of <see cref="CityVisitDTO"/> representing trending cities and their visit counts.
    /// Each <see cref="CityVisitDTO"/> object includes the city name and the number of visits, indicating its popularity within the specified time frame.
    /// </returns>
    Task<IEnumerable<CityVisitDTO>> GetTrendingCities(
        VisitTimeOptionQuery timeQuery,
        PaginationDTO pagination);
}