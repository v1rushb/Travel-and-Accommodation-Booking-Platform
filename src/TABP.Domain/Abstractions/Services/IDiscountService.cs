using TABP.Domain.Models.Discount;
using TABP.Domain.Models.Discount.Search;
using TABP.Domain.Models.Discount.Search.Response;
using TABP.Domain.Models.Discount.Sort;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services;

public interface IDiscountService
{
    Task<DiscountDTO> GetByIdAsync(Guid Id);
    Task AddAsync(DiscountDTO newDiscount);
    Task UpdateAsync(DiscountDTO updatedDiscount);
    Task DeleteAsync(Guid Id);
    Task<bool> ExistsAsync(Guid Id);
    // Task<IEnumerable<Discount>> GetByHotelAsync(Guid hotelId);
    Task<IEnumerable<DiscountForAdminResponseDTO>> SearchForAdminAsync(
        DiscountSearchQuery query,
        PaginationDTO pagination,
        DiscountSortQuery sortQuery);
    Task<IEnumerable<DiscountDTO>> GetActiveDiscountsForHotelAsync(Guid hotelId);
}