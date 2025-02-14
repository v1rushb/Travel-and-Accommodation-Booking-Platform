using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Cart;
using TABP.Domain.Models.Cart.Search.Response;
using TABP.Domain.Models.CartItem;

namespace TABP.Domain.Abstractions.Repositories;

/// <summary>
/// Defines the contract for a repository to manage <see cref="Cart"/> entities.
/// This interface provides asynchronous operations for creating, retrieving, updating, and deleting cart data,
/// as well as managing cart items within a cart, and searching for carts based on various criteria.
/// </summary>
public interface ICartRepository
{
    /// <summary>
    /// Asynchronously creates a new shopping cart in the repository.
    /// </summary>
    /// <param name="cart">A <see cref="CartDTO"/> containing the data for the new cart.</param>
    /// <returns>A <see cref="Task{Guid}"/> representing the asynchronous operation, and upon completion,
    /// returns the unique identifier of the newly created cart.</returns>
    Task<Guid> CreateAsync(CartDTO cart);

    /// <summary>
    /// Asynchronously deletes a cart from the repository by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the cart to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteAsync(Guid Id);

    /// <summary>
    /// Asynchronously updates an existing cart in the repository.
    /// </summary>
    /// <param name="updatedCart">A <see cref="CartDTO"/> containing the updated data for the cart.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateAsync(CartDTO updatedCart);

    /// <summary>
    /// Asynchronously checks if a cart with the specified ID exists in the repository.
    /// </summary>
    /// <param name="Id">The unique identifier of the cart to check.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion,
    /// returns <c>true</c> if a cart with the given ID exists; otherwise, <c>false</c>.</returns>
    Task<bool> ExistsAsync(Guid Id);

    /// <summary>
    /// Asynchronously retrieves a cart from the repository by its unique identifier, including its items.
    /// </summary>
    /// <param name="Id">The unique identifier of the cart to retrieve.</param>
    /// <returns>A <see cref="Task{CartDTO}"/> representing the asynchronous operation, and upon completion,
    /// returns the <see cref="CartDTO"/> if found, including its items; otherwise, <c>null</c>.</returns>
    Task<CartDTO> GetByIdAsync(Guid Id);

    /// <summary>
    /// Asynchronously retrieves the last pending cart for a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A <see cref="Task{CartDTO}"/> representing the asynchronous operation, and upon completion,
    /// returns the <see cref="CartDTO"/> representing the last pending cart for the user, or <c>null</c> if no pending cart is found.</returns>
    Task<CartDTO> GetLastPendingCartAsync(Guid userId);

    /// <summary>
    /// Asynchronously adds a new item to a shopping cart.
    /// </summary>
    /// <param name="newCartItem">A <see cref="CartItemDTO"/> containing the data for the new cart item to add.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task AddItemAsync(CartItemDTO newCartItem);

    /// <summary>
    /// Asynchronously deletes an item from a shopping cart.
    /// </summary>
    /// <param name="cartItemId">The unique identifier of the cart item to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteItemAsync(Guid cartItemId);

    /// <summary>
    /// Asynchronously updates an item within a shopping cart.
    /// </summary>
    /// <param name="cartItem">A <see cref="CartItemDTO"/> containing the updated data for the cart item.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateItemAsync(CartItemDTO cartItem);

    /// <summary>
    /// Asynchronously retrieves all items within a specific shopping cart, with pagination.
    /// </summary>
    /// <param name="cartId">The unique identifier of the cart.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of cart items per page.</param>
    /// <returns>A <see cref="Task{IEnumerable{CartItemDTO}}"/> representing the asynchronous operation, and upon completion, returns a collection of <see cref="CartItemDTO"/> representing the items in the specified cart, paginated.</returns>
    Task<IEnumerable<CartItemDTO>> GetAllCartItemsAsync(
        Guid cartId,
        int pageNumber,
        int pageSize
    );

    /// <summary>
    /// Asynchronously checks if a room is booked for the same user within a specified date range in a pending cart.
    /// </summary>
    /// <param name="roomId">The unique identifier of the room.</param>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="startingDate">The starting date of the date range.</param>
    /// <param name="endingDate">The ending date of the date range.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion, returns <c>true</c> if the room is booked by the same user within the specified date range in a pending cart; otherwise, <c>false</c>.</returns>
    Task<bool> RoomIsBookedForSameUserBetween(
        Guid roomId,
        Guid userId,
        DateTime startingDate,
        DateTime endingDate
    );

    /// <summary>
    /// Asynchronously checks if a room is booked by any user within a specified date range in a confirmed cart.
    /// </summary>
    /// <param name="roomId">The unique identifier of the room.</param>
    /// <param name="startingDate">The starting date of the date range.</param>
    /// <param name="endingDate">The ending date of the date range.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion, returns <c>true</c> if the room is booked by any user within the specified date range in a confirmed cart; otherwise, <c>false</c>.</returns>
    Task<bool> RoomIsBookedByAnyUserBetween(
        Guid roomId,
        DateTime startingDate,
        DateTime endingDate
    );

    /// <summary>
    /// Asynchronously searches for carts in the repository based on a predicate, with pagination and optional sorting, for admin users.
    /// </summary>
    /// <param name="predicate">An <see cref="Expression{Func{Cart, bool}}"/> that defines the search conditions for carts.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of carts per page.</param>
    /// <param name="orderByDelegate">An optional <see cref="Func{IQueryable{Cart}, IOrderedQueryable{Cart}}"/> delegate to specify the sorting order.</param>
    /// <returns>A <see cref="Task{IEnumerable{CartAdminResponseDTO}}"/> representing the asynchronous operation, and upon completion, returns a collection of <see cref="CartAdminResponseDTO"/> that match the search criteria, tailored for admin responses.</returns>
    Task<IEnumerable<CartAdminResponseDTO>> SearchAdminAsync(
        Expression<Func<Cart, bool>> predicate,
        int pageNumber,
        int pageSize,
        Func<IQueryable<Cart>, IOrderedQueryable<Cart>> orderByDelegate = null);

    /// <summary>
    /// Asynchronously retrieves cart details for a specific user, tailored for user responses.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A <see cref="Task{CartUserResponseDTO}"/> representing the asynchronous operation, and upon completion, returns a <see cref="CartUserResponseDTO"/> containing cart details for the specified user, or <c>null</c> if no cart is found.</returns>
    Task<CartUserResponseDTO> GetCartDetailsByUserIdAsync(Guid userId);
}