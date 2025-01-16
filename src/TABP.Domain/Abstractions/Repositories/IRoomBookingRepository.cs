using TABP.Domain.Entities;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Domain.Abstractions.Repositories;

public interface IRoomBookingRepository
{
    Task<Guid> AddAsync(RoomBookingDTO newBooking);
    Task UpdateAsync(RoomBookingDTO updatedBooking);
    Task DeleteAsync(Guid Id);
    Task<RoomBooking?> GetByIdAsync(Guid Id);
    Task<bool> ExistsAsync(Guid Id);
    Task<IEnumerable<RoomBooking>> GetByUserAsync(Guid userId);
    Task<IEnumerable<RoomBooking>> GetByRoomAsync(Guid roomId);
    Task<bool> RoomIsBookedBetween(Guid roomId, DateTime StartingDate, DateTime EndingDate);
}