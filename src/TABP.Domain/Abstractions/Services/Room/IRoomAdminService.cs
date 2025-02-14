using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Room.Search;
using TABP.Domain.Models.Room.Search.Response;
using TABP.Domain.Models.Room.Sort;

namespace TABP.Domain.Abstractions.Services;


/// <summary>
/// Defines methods for performing administrative operations.
/// This includes searching for rooms using various filtering, sorting and pagination options.
/// </summary>
public interface IRoomAdminService
{
    /// <summary>
    /// Searches for rooms based on the specified criteria.
    /// </summary>
    /// <param name="query">The search query containing filtering options.</param>
    /// <param name="pagination">The pagination parameters to control the result set.</param>
    /// <param name="sortQuery">The sorting criteria to order the results.</param>
    /// <returns>
    /// A collection of<see cref="RoomAdminResponseDTO"/>representing rooms.
    /// </returns>
    /// <exception cref="FluentValidation.ValidationException">
    /// Thrown if the pagination or sorting parameters are invalid.
    /// </exception>
    Task<IEnumerable<RoomAdminResponseDTO>> SearchAsync(
        RoomSearchQuery query,
        PaginationDTO pagination,
        RoomSortQuery sortQuery);
}