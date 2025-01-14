using TABP.Domain.Entities;
using TABP.Domain.Models.Discount;

namespace TABP.Domain.Abstractions.Repositories;

public interface IDiscountRepository
{
    Task<Guid> AddAsync(DiscountDTO newDiscount); 
    Task<DiscountDTO?> GetByIdAsync(Guid Id); // idk change this to discount based on ur need. (change mappings too)
    Task UpdateAsync(DiscountDTO updatedDiscount);
    Task DeleteAsync(Guid Id);
    Task<bool> ExistsAsync(Guid Id);
}