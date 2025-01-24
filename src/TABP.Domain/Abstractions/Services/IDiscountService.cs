using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Models.Discount;
using TABP.Domain.Models.Discount.Search;
using TABP.Domain.Models.Discount.Search.Response;
using TABP.Domain.Models.Pagination;

namespace TABP.Abstractions.Services;

public interface IDiscountService
{
    Task<DiscountDTO> GetByIdAsync(Guid Id);
    Task<Guid> AddAsync(DiscountDTO newDiscount);
    Task UpdateAsync(DiscountDTO updatedDiscount);
    Task DeleteAsync(Guid Id);
    Task<bool> ExistsAsync(Guid Id);
    Task<IEnumerable<Discount>> GetByHotelAsync(Guid hotelId);
    Task<IEnumerable<DiscountForAdminResponseDTO>> SearchForAdminAsync(DiscountSearchQuery query, PaginationDTO pagination);
    Task<IEnumerable<DiscountDTO>> GetActiveDiscountsForHotelAsync(Guid hotelId);
}