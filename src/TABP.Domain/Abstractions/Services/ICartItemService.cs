using TABP.Domain.Entities;
using TABP.Domain.Models.CartItem;

namespace TABP.Domain.Abstractions.Services;

public interface ICartItemService
{
    Task<Guid> AddAsync(CartItemDTO newCartItem);
    Task DeleteAsync(Guid Id);
    Task<CartItemDTO> GetByIdAsync(Guid Id);
    Task<bool> ExistsAsync(Guid Id);
    // Task<IEnumerable<CartItem>> GetByUserAsync(Guid userId);
}