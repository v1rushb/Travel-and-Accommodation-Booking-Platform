using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Discount;
using TABP.Domain.Models.Discount.Search.Response;

namespace TABP.Domain.Abstractions.Repositories;

public interface IDiscountRepository
{
    Task<Guid> AddAsync(DiscountDTO newDiscount); 
    Task<DiscountDTO> GetByIdAsync(Guid Id);
    Task UpdateAsync(DiscountDTO updatedDiscount);
    Task DeleteAsync(Guid Id);
    Task<bool> ExistsAsync(Guid Id);
    Task<IEnumerable<Discount>> GetByHotelAsync(Guid hotelId);
    Task<DiscountDTO> GetHighestDiscountActiveForHotelAsync(Guid hotelId);
    Task<IEnumerable<DiscountForAdminResponseDTO>> SearchForAdminAsync(Expression<Func<Discount, bool>> expression, int pageNumber, int pageSize);
}