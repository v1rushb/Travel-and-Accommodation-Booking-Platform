using TABP.Domain.Entities;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Room;
using TABP.Domain.Models.Room.Search;
using TABP.Domain.Models.Room.Search.Response;

namespace TABP.Domain.Abstractions.Services;

public interface IRoomService
{
    Task<Guid> AddAsync(RoomDTO newRoom);
    Task DeleteAsync(Guid Id);
    Task UpdateAsync(RoomDTO updatedRoom);
    Task<bool> ExistsAsync(Guid Id);
    Task<RoomDTO> GetByIdAsync(Guid Id);
    Task<IEnumerable<Room>> GetRoomsByHotelAsync(Guid hotelId);
    Task<bool> RoomNumberExistsForHotelAsync(Guid hotelId, Guid RoomId);
    Task<IEnumerable<RoomAdminResponseDTO>> SearchAdminAsync(RoomSearchQuery query, PaginationDTO pagination);
}