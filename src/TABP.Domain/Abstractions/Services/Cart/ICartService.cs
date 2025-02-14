using TABP.Domain.Models.Cart;
using TABP.Domain.Models.Cart.Search.Response;
using TABP.Domain.Models.CartItem;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services.Cart;

/// <summary>
/// Defines core operations for managing user shopping carts.
/// This interface specifies the essential functionalities for handling shopping carts,
/// including creation, retrieval, item management, and checkout processes from a user perspective.
/// </summary>
public interface ICartService
{
    /// <summary>
    /// Creates a new shopping cart.
    /// This method initializes a new cart record in the system, typically when a user starts shopping
    /// without an existing active cart. It sets up the basic cart structure and associates it with the current user.
    /// </summary>
    /// <returns>
    /// <see cref="CartDTO"/> representing the newly created shopping cart.
    /// This DTO contains the unique identifier of the cart and its initial state, ready for adding items.
    /// </returns>
    Task<CartDTO> CreateNewAsync();

    /// <summary>
    /// Retrieves the user's pending shopping cart or creates a new one if none exists.
    /// This method is used to ensure that a user always has an active cart when they are shopping.
    /// If a pending cart (i.e., a cart that has not been checked out yet) exists for the user, it is retrieved.
    /// Otherwise, a new cart is created and returned.
    /// </summary>
    /// <returns>
    /// <see cref="CartDTO"/> representing the user's current pending shopping cart.
    /// If a cart exists, it is returned; otherwise, a newly created cart is returned.
    /// </returns>
    Task<CartDTO> GetOrCreatePendingCartAsync();

    /// <summary>
    /// Adds an item to the shopping cart.
    /// This method allows users to add products or services to their current shopping cart.
    /// It handles the association of the item with the cart and updates the cart's content.
    /// </summary>
    /// <param name="newCartItem">
    /// <see cref="CartItemDTO"/> containing details of the item to be added to the cart.
    /// This DTO includes information such as the product or service ID, quantity, and any other relevant details.
    /// </param>
    Task AddItemAsync(CartItemDTO newCartItem);

    /// <summary>
    /// Deletes an item from the shopping cart.
    /// This method removes a specific item from the user's shopping cart.
    /// It is used when a user decides to remove an item they have previously added to their cart.
    /// </summary>
    /// <param name="cartItemId">
    /// The unique <see cref="Guid"/> identifier of the cart item to be removed.
    /// This ID specifies which item should be deleted from the cart.
    /// </param>
    Task DeleteItemAsync(Guid cartItemId);

    /// <summary>
    /// Processes the checkout for the current shopping cart.
    /// This method finalizes the shopping process, converting the cart into an order.
    /// It typically involves steps such as payment processing, inventory updates, and order confirmation.
    /// </summary>
    Task CheckOutAsync();

    /// <summary>
    /// Retrieves a paginated list of items in the current shopping cart.
    /// This method is used to display the contents of the shopping cart to the user, allowing them to review
    /// the items they have added. It supports pagination to handle carts with a large number of items efficiently.
    /// </summary>
    /// <param name="pagination">
    /// The pagination parameters that control the result set size and page number.
    /// Used to manage the display of cart items in a paginated manner, especially when there are many items in the cart.
    /// </param>
    /// <returns>
    /// A collection of <see cref="CartItemDTO"/> representing the items in the shopping cart for the current page.
    /// Each <see cref="CartItemDTO"/> object contains details about a specific item in the cart, such as product information and quantity.
    /// </returns>
    Task<IEnumerable<CartItemDTO>> GetCartItemsAsync(PaginationDTO pagination);

    /// <summary>
    /// Retrieves detailed information about the user's current shopping cart, including items and total price.
    /// This method provides a comprehensive view of the cart's contents, typically used for displaying
    /// a cart summary to the user. It calculates and returns the total price of all items in the cart.
    /// </summary>
    /// <returns>
    /// <see cref="CartUserResponseDTO"/> representing the details of the user's current shopping cart.
    /// This DTO includes a list of cart items and the total price, providing a complete overview of the cart's state.
    /// </returns>
    Task<CartUserResponseDTO> GetCurrentCartDetailsAsync();
    
}