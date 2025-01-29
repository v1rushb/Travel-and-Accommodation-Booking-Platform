using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Room.Search;
using TABP.Domain.Models.Room.Search.Response;

namespace TABP.Domain.Abstractions.Services;

public interface IRoomAdminService
{
    // Task<IEnumerable<Room>> GetRoomsByHotelAsync(Guid hotelId); // check later.
    Task<IEnumerable<RoomAdminResponseDTO>> SearchAsync(RoomSearchQuery query, PaginationDTO pagination);
}