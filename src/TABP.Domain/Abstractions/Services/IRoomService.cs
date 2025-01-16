using TABP.Domain.Entities;
using TABP.Domain.Models.Room;

namespace TABP.Domain.Abstractions.Services;

public interface IRoomService
{
    Task<Guid> AddAsync(RoomDTO newRoom);
    Task DeleteAsync(Guid Id);
    Task UpdateAsync(RoomDTO updatedRoom);
    Task<bool> ExistsAsync(Guid Id);
    Task<Room> GetByIdAsync(Guid Id);
    Task<IEnumerable<Room>> GetRoomsByHotelAsync(Guid hotelId);
    Task<bool> RoomNumberExistsForHotelAsync(Guid hotelId, Guid RoomId);
}