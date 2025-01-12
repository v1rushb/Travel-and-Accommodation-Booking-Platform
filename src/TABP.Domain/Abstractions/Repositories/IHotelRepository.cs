using TABP.Domain.Entities;

namespace TABP.Domain.Abstractions.Repositories;

public interface IHotelRepository
{
    Task<Guid> AddAsync(Hotel newHotel);
    Task<bool> ExistsAsync(Guid Id);
    Task DeleteAsync(Guid Id);
    Task UpdateAsync(Guid Id);
    Task<Hotel?> GetByIdAsync(Guid Id);
}