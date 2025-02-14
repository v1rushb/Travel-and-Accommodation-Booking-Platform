using TABP.Domain.Models.City.Response;
using TABP.Domain.Models.City.Search;
using TABP.Domain.Models.City.Sort;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services.City;


/// <summary>
/// Defines administrative operations for managing cities.
/// allowing for filtering, pagination, and sorting to manage city data effectively.
/// </summary>
public interface ICityAdminService
{
    /// <summary>
    /// Searches for cities based on the specified criteria, tailored for administrative use.
    /// This method supports filtering, pagination, and sorting to retrieve a list of cities
    /// that match the given search parameters. It is designed for administrative interfaces
    /// where detailed management of city records is required.
    /// </summary>
    /// <param name="query">
    /// The city search query containing filtering options.
    /// This parameter allows for specifying criteria to narrow down the search results,
    /// such as city name, country, or other relevant attributes.
    /// </param>
    /// <param name="pagination">
    /// The pagination parameters that control the result set size and page number.
    /// Used to manage large datasets by dividing results into pages, improving performance
    /// and user experience when dealing with numerous city records.
    /// </param>
    /// <param name="sortQuery">
    /// The sorting parameters that determine the order of the results.
    /// Allows administrators to sort cities based on different fields such as name, creation date, etc.,
    /// in ascending or descending order to facilitate data review and management.
    /// </param>
    /// <returns>
    /// A collection of <see cref="CityAdminResponseDTO"/> representing cities that match the search criteria.
    /// Each <see cref="CityAdminResponseDTO"/> object contains detailed information about a city,
    /// suitable for display in administrative interfaces.
    /// </returns>
    Task<IEnumerable<CityAdminResponseDTO>> SearchAsync(
        CitySearchQuery query,
        PaginationDTO pagination,
        CitySortQuery sortQuery);
}