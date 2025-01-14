using TABP.Domain.Entities;
using TABP.Domain.Models.Hotels;

namespace TABP.Domain.Abstractions.Repositories;

public interface IHotelRepository
{
    Task<Guid> AddAsync(HotelDTO newHotel);
    Task<bool> ExistsAsync(Guid Id);
    Task DeleteAsync(Guid Id);
    Task UpdateAsync(HotelDTO updatedHotel);
    Task<Hotel?> GetByIdAsync(Guid Id);
}