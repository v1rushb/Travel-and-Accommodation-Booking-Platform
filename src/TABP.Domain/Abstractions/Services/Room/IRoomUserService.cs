using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Room.Search;
using TABP.Domain.Models.Room.Search.Response;
using TABP.Domain.Models.Room.Sort;

namespace TABP.Domain.Abstractions.Services;


/// <summary>
/// Provides operations for retrieving Room details meant for end users.
/// This includes searching for rooms using filtering, sorting, and pagination.
/// </summary>
public interface IRoomUserService
{
    /// <summary>
    /// Searches for rooms based on the specified criteria.
    /// </summary>
    /// <param name="query">The search query containing filtering options.</param>
    /// <param name="pagination">The pagination parameters to control the result set.</param>
    /// <param name="sortQuery">The sorting criteria to order the results.</param>
    /// <returns>
    /// A collection of<see cref="RoomUserResponseDTO"/>Representing Rooms.
    /// </returns>
    /// <exception cref="FluentValidation.ValidationException">
    /// Thrown if the pagination or sorting parameters are invalid.
    /// </exception>
    Task<IEnumerable<RoomUserResponseDTO>> SearchAsync(
        RoomSearchQuery query,
        PaginationDTO pagination,
        RoomSortQuery sortQuery);
}