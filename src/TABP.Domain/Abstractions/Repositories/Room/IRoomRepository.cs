using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Room;
using TABP.Domain.Models.Room.Search.Response;

namespace TABP.Domain.Abstractions.Repositories;

public interface IRoomRepository
{
    Task<Guid> AddAsync(RoomDTO newRoom);
    Task<bool> ExistsAsync(Guid Id);
    Task DeleteAsync(Guid Id);
    Task UpdateAsync(RoomDTO updatedRoom);
    Task<RoomDTO> GetByIdAsync(Guid Id); 

    Task<IEnumerable<Room>> GetRoomsByHotelAsync(Guid HotelId);

    Task<bool> RoomExistsForHotelAsync(Guid HotelId, Guid RoomId);
    Task<IEnumerable<RoomDTO>> SearchAsync(Expression<Func<Room, bool>> predicate, int pageNumber, int pageSize);
}