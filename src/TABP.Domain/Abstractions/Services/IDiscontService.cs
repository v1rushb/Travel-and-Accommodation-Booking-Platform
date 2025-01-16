using TABP.Domain.Entities;
using TABP.Domain.Models.Discount;

namespace TABP.Abstractions.Services;

public interface IDiscountService
{
    Task<Discount> GetByIdAsync(Guid Id);
    Task<Guid> AddAsync(DiscountDTO newDiscount);
    Task UpdateAsync(DiscountDTO updatedDiscount);
    Task DeleteAsync(Guid Id);
    Task<bool> ExistsAsync(Guid Id);
    Task<IEnumerable<Discount>> GetByHotelAsync(Guid hotelId); 
}