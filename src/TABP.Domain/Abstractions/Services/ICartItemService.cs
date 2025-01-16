using TABD.Domain.Models.CartItem;
using TABP.Domain.Entities;

namespace TABP.Domain.Abstractions.Services;

public interface ICartItemService
{
    Task<Guid> AddAsync(CartItemDTO newCartItem);
    Task DeleteAsync(Guid Id);
    Task<CartItem> GetByIdAsync(Guid Id);
    Task<bool> ExistsAsync(Guid Id);
    Task<IEnumerable<CartItem>> GetByUserAsync(Guid userId);
}