using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Room.Search;
using TABP.Domain.Models.Room.Search.Response;
using TABP.Domain.Models.Room.Sort;

namespace TABP.Domain.Abstractions.Services;

public interface IRoomUserService
{
    Task<IEnumerable<RoomUserResponseDTO>> SearchAsync(
        RoomSearchQuery query,
        PaginationDTO pagination,
        RoomSortQuery sortQuery);
}