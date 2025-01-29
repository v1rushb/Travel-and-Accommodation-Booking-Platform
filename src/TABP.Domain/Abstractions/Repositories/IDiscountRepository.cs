using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
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
    // Task<IEnumerable<Discount>> GetByHotelAsync(Guid hotelId);
    Task<DiscountDTO> GetHighestDiscountActiveForHotelRoomTypeAsync(Guid hotelId, RoomType type);
    //change to searchasync only.
    Task<IEnumerable<DiscountForAdminResponseDTO>> SearchForAdminAsync(Expression<Func<Discount, bool>> expression, int pageNumber, int pageSize);
    Task<IEnumerable<DiscountDTO>> GetActiveDiscountsForHotelAsync(Guid hotelId);
}