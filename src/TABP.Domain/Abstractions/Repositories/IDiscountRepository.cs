using TABP.Domain.Entities;

namespace TABP.Domain.Abstractions.Repositories;

public interface IDiscountRepository
{
    Task<Guid> AddAsync(Discount newDiscount); 
}