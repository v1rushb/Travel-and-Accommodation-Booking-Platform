using TABP.Domain.Models.CartItem;

namespace TABP.Domain.Abstractions.Repositories;

/// <summary>
/// Defines the contract for a repository to manage <see cref="CartItem"/> entities.
/// This interface provides asynchronous operations for creating, retrieving, updating, and deleting cart item data,
/// as well as checking for the existence of cart items.
/// </summary>
public interface ICartItemRepository
{
    /// <summary>
    /// Asynchronously adds a new cart item to the repository.
    /// </summary>
    /// <param name="newCartItem">A <see cref="CartItemDTO"/> containing the data for the new cart item.</param>
    /// <returns>A <see cref="Task{Guid}"/> representing the asynchronous operation, and upon completion,
    /// returns the unique identifier of the newly added cart item.
    /// </returns>
    Task<Guid> AddAsync(CartItemDTO newCartItem);

    /// <summary>
    /// Asynchronously deletes a cart item from the repository by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the cart item to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteAsync(Guid Id);

    /// <summary>
    /// Asynchronously checks if a cart item with the specified ID exists in the repository.
    /// </summary>
    /// <param name="Id">The unique identifier of the cart item to check.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion,
    /// returns <c>true</c> if a cart item with the given ID exists; otherwise, <c>false</c>.</returns>
    Task<bool> ExistsAsync(Guid Id);

    /// <summary>
    /// Asynchronously retrieves a cart item from the repository by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the cart item to retrieve.</param>
    /// <returns>A <see cref="Task{CartItemDTO}"/> representing the asynchronous operation, and upon completion,
    /// returns the <see cref="CartItemDTO"/> if found; otherwise, <c>null</c>.</returns>
    Task<CartItemDTO> GetByIdAsync(Guid Id);

    /// <summary>
    /// Updates a collection of cart items in the repository. This is a void return synchronous operation.
    /// </summary>
    /// <param name="cartItems">An <see cref="IEnumerable{CartItemDTO}"/> containing the updated data for the cart items.</param>
    void Update(IEnumerable<CartItemDTO> cartItems);
}