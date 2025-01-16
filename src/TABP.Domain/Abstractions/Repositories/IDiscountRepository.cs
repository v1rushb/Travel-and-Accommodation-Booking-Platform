using TABP.Domain.Entities;
using TABP.Domain.Models.Discount;

namespace TABP.Domain.Abstractions.Repositories;

public interface IDiscountRepository
{
    Task<Guid> AddAsync(DiscountDTO newDiscount); 
    Task<Discount?> GetByIdAsync(Guid Id);
    Task UpdateAsync(DiscountDTO updatedDiscount);
    Task DeleteAsync(Guid Id);
    Task<bool> ExistsAsync(Guid Id);
    Task<IEnumerable<Discount>> GetByHotelAsync(Guid hotelId);
}