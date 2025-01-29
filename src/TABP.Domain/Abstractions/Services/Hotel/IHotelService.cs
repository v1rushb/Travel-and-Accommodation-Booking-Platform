using TABP.Domain.Models.Hotel;

namespace TABP.Domain.Abstractions.Services.Hotel;

public interface IHotelService
{
    Task AddAsync(HotelDTO newHotel);
    Task UpdateAsync(HotelDTO updatedHotel);
    Task DeleteAsync(Guid Id);
    Task<HotelDTO> GetByIdAsync(Guid Id);
    Task<bool> ExistsAsync(Guid Id);
    Task<int> GetNextRoomNumberAsync(Guid hotelId);
}