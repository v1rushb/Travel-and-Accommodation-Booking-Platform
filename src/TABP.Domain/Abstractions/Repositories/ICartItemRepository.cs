using TABD.Domain.Models.CartItem;
using TABP.Domain.Entities;

namespace TABP.Domain.Abstractions.Repositories;

public interface ICartItemRepository
{
    Task<Guid> AddAsync(CartItemDTO newCartItem);
    Task DeleteAsync(Guid Id);
    Task<bool> ExistsAsync(Guid Id);
    Task<CartItem?> GetByIdAsync(Guid Id);
    Task<bool> ExistsBetweenUserAndRoomAsync(Guid userId, Guid roomId);
    Task<IEnumerable<CartItem>> GetByUserAsync(Guid userId);
}