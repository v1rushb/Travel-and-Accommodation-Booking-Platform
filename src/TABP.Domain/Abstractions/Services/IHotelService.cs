using TABP.Domain.Entities;
using TABP.Domain.Models.Hotels;

namespace TABP.Domain.Abstractions.Services;

public interface IHotelService
{
    Task<Guid> AddAsync(HotelDTO newHotel);
    Task UpdateAsync(HotelDTO updatedHotel);
    Task DeleteAsync(Guid Id);
    Task<Hotel> GetByIdAsync(Guid Id);
    Task<bool> ExistsAsync(Guid Id);
}