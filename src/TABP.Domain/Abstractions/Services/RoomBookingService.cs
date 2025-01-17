using TABP.Domain.Entities;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Domain.Abstractions.Services;

public interface IRoomBookingService
{
    Task<Guid> AddAsync(RoomBookingDTO newBooking);
    Task UpdateAsync(RoomBookingDTO updatedBooking);
    Task DeleteAsync(Guid Id);
    Task<RoomBooking?> GetByIdAsync(Guid Id);
    Task<bool> ExistsAsync(Guid Id);
    Task<IEnumerable<RoomBooking>> GetByUserAsync();
    Task<IEnumerable<RoomBooking>> GetByRoomAsync(Guid roomId);
    Task<bool> RoomIsBookedBetween(Guid roomId, DateTime StartingDate, DateTime EndingDate);
}