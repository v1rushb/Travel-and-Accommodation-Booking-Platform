using TABP.Domain.Models.Room;

namespace TABP.Domain.Abstractions.Services;

public interface IRoomService
{
    Task AddAsync(RoomDTO newRoom);

    Task DeleteAsync(Guid Id);

    Task UpdateAsync(RoomDTO updatedRoom);

    Task<bool> ExistsAsync(Guid Id);

    Task<RoomDTO> GetByIdAsync(Guid Id);

    Task<bool> RoomNumberExistsForHotelAsync(
        Guid hotelId,
        Guid RoomId);

    Task<decimal> GetBookingPriceForRoom(
        Guid RoomId,
        DateTime checkInDate,
        DateTime checkOutDate);
}