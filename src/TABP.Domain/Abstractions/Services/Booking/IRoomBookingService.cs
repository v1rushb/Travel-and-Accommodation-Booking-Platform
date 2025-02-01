using TABP.Domain.Models.Booking;
using TABP.Domain.Models.Cart;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Domain.Abstractions.Services.Booking;

public interface IRoomBookingService
{
    Task AddAsync(CartDTO Cart);
    Task<RoomBookingDTO> GetByIdAsync(Guid Id);
    Task<bool> ExistsAsync(Guid Id);
    // Task<IEnumerable<RoomBooking>> GetByUserAsync();
    // Task<IEnumerable<RoomBooking>> GetByRoomAsync(Guid roomId);
    Task<bool> RoomIsBookedBetween(Guid roomId, DateTime StartingDate, DateTime EndingDate);
    Task<IEnumerable<HotelBookingDTO>> GetByHotelAsync();
}