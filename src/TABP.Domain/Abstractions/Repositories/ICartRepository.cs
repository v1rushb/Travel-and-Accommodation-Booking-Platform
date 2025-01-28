using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Cart;
using TABP.Domain.Models.Cart.Search;
using TABP.Domain.Models.Cart.Search.Response;
using TABP.Domain.Models.CartItem;

namespace TABP.Domain.Abstractions.Repositories;

public interface ICartRepository
{
    Task<Guid> CreateAsync(CartDTO cart);
    Task DeleteAsync(Guid Id);
    Task UpdateAsync(CartDTO updatedCart);
    Task<bool> ExistsAsync(Guid Id);
    Task<CartDTO> GetByIdAsync(Guid Id);
    Task<CartDTO> GetLastPendingCartAsync(Guid userId);
    Task AddItemAsync(CartItemDTO newCartItem);
    Task DeleteItemAsync(Guid cartItemId);
    Task UpdateItemAsync(CartItemDTO cartItem);

    Task<IEnumerable<CartItemDTO>> GetAllCartItemsAsync(Guid cartId, int pageNumber, int pageSize);
    Task<bool> RoomIsBookedForSameUserBetween(Guid roomId, Guid userId, DateTime StartingDate, DateTime EndingDate);
    Task<bool> RoomIsBookedByAnyUserBetween(Guid roomId, DateTime StartingDate, DateTime EndingDate);
    Task<IEnumerable<CartAdminResponseDTO>> SearchAdminAsync(Expression<Func<Cart, bool>> predicate, int pageNumber, int pageSize);
    Task<CartUserResponseDTO> GetCartDetailsByUserIdAsync(Guid userId);
}