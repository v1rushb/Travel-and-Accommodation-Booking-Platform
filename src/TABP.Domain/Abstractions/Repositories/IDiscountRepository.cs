using TABP.Domain.Entities;

namespace TABP.Domain.Abstractions.Repositories;

public interface IDiscountRepository
{
    Task<Guid> AddAsync(Discount newDiscount); 
    Task<Discount?> GetByIdAsync(Guid Id);
    Task<IEnumerable<Discount>> GetAllAsync();
    Task UpdateAsync(Discount updatedDiscount);
    Task DeleteAsync(Guid Id);
}