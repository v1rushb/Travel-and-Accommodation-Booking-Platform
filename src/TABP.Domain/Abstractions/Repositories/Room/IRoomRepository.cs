using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Room;

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
    Task<IEnumerable<RoomDTO>> SearchAsync(
        Expression<Func<RoomWithAvailability, bool>> predicate,
        int pageNumber,
        int pageSize,
        Func<IQueryable<RoomWithAvailability>, IOrderedQueryable<RoomWithAvailability>> orderBy = null);
}