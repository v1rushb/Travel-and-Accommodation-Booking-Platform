using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Booking;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Domain.Abstractions.Repositories;

public interface IRoomBookingRepository
{
    Task AddAsync(IEnumerable<RoomBookingDTO> bookings);
    Task<RoomBookingDTO> GetByIdAsync(Guid Id);
    Task<bool> ExistsAsync(Guid Id);
    // Task<IEnumerable<RoomBooking>> GetByUserAsync(Guid userId);
    // Task<IEnumerable<RoomBooking>> GetByRoomAsync(Guid roomId);
    Task<bool> RoomIsBookedBetween(Guid roomId, DateTime StartingDate, DateTime EndingDate);
    Task<IEnumerable<RoomBookingDTO>> SearchAsync(Expression<Func<RoomBooking, bool>> predicate, int pageNumber, int pageSize);
    // Task<IEnumerable<BookingAdminResponseDTO>> SearchAdminAsync(Expression<Func<RoomBooking, bool>> predicate, int pageNumber, int pageSize);
    Task<IEnumerable<HotelBookingDTO>> GetAllForHotelsAsync(Expression<Func<RoomBooking, bool>> predicate);
}