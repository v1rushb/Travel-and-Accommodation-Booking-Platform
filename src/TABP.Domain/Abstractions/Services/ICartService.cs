using TABP.Domain.Entities;
using TABP.Domain.Models.Cart;
using TABP.Domain.Models.CartItem;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services;

public interface ICartService
{
    Task<CartDTO> CreateNewAsync();
    Task<CartDTO> GetOrCreatePendingCartAsync();
    Task AddItemAsync(CartItemDTO newCartItem);
    Task DeleteItemAsync(Guid cartItemId);
    Task CheckOutAsync();
    Task<IEnumerable<CartItemDTO>> GetCartItemsAsync(PaginationDTO pagination);
}