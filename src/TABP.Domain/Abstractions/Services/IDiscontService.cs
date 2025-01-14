using TABP.Domain.Entities;
using TABP.Domain.Models.Discount;

namespace TABP.Abstractions.Services;

public interface IDiscountService
{
    Task<Discount> GetByIdAsync(Guid Id);
    Task<Discount> AddAsync(DiscountDTO newDiscount);
    Task UpdateAsync(DiscountDTO updatedDiscount);
    Task DeleteAsync(Guid Id);
}