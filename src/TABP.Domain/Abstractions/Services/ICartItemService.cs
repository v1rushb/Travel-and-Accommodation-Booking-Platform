using TABP.Domain.Models.CartItem;

namespace TABP.Domain.Abstractions.Services;

/// <summary>
/// Defines main processing operations for managing and 
/// direct interacting with CartItem Repository.
/// </summary>
public interface ICartItemService
{
    /// <summary>
    /// Adds a new cart item.
    /// </summary>
    /// <param name="newCartItem">An object containing the details of the cart item to add.</param>
    Task AddAsync(CartItemDTO newCartItem);


    /// <summary>
    /// Deletes a cart item by its unique<see cref="Guid"/>identifier.
    /// </summary>
    /// <param name="Id">The unique<see cref="Guid"/>identifier of the cart item to delete.</param>
    Task DeleteAsync(Guid Id);

     /// <summary>
    /// Retrieves a cart item by its unique<see cref="Guid"/>identifier.
    /// </summary>
    /// <param name="Id">The unique<see cref="Guid"/>identifier of the cart item.</param>
    /// <returns>
    /// <see cref="CartItemDTO"/>containing the cart item details.
    /// </returns>
    /// <exception cref="Exceptions.EntityNotFoundException">
    /// Thrown when the CartItem does not exist.
    /// </exception>
    Task<CartItemDTO> GetByIdAsync(Guid Id);

    /// <summary>
    /// Checks if a cart item exists by its unique<see cref="Guid"/>identifier.
    /// </summary>
    /// <param name="Id">The unique<see cref="Guid"/>identifier of the cart item.</param>
    /// <returns><c>true</c> if the user exists; otherwise, <c>false</c>.</returns>
    Task<bool> ExistsAsync(Guid Id);
}