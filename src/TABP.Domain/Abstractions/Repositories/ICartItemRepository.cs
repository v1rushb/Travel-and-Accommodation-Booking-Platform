using TABP.Domain.Models.CartItem;

namespace TABP.Domain.Abstractions.Repositories;

public interface ICartItemRepository
{
    Task<Guid> AddAsync(CartItemDTO newCartItem);
    Task DeleteAsync(Guid Id);
    Task<bool> ExistsAsync(Guid Id);
    Task<CartItemDTO> GetByIdAsync(Guid Id);
    void Update(IEnumerable<CartItemDTO> cartItems);
    // Task<bool> ExistsBetweenUserAndRoomAsync(Guid userId, Guid roomId);
    // Task<IEnumerable<CartItem>> GetByUserAsync(Guid userId);
}