using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Models.Discount;
using TABP.Domain.Models.Discount.Search.Response;

namespace TABP.Domain.Abstractions.Repositories;

/// <summary>
/// Defines the contract for a repository to manage <see cref="Discount"/> entities.
/// This interface provides asynchronous operations for creating, retrieving, updating, and deleting discount data,
/// as well as searching for discounts and fetching the highest active discount for a hotel room type.
/// </summary>
public interface IDiscountRepository
{
    /// <summary>
    /// Asynchronously adds a new discount to the repository.
    /// </summary>
    /// <param name="newDiscount">A <see cref="DiscountDTO"/> containing the data for the new discount.</param>
    /// <returns>A <see cref="Task{Guid}"/> representing the asynchronous operation, and upon completion,
    /// returns the unique identifier of the newly added discount.
    /// </returns>
    Task<Guid> AddAsync(DiscountDTO newDiscount);

    /// <summary>
    /// Asynchronously retrieves a discount from the repository by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the discount to retrieve.</param>
    /// <returns>A <see cref="Task{DiscountDTO}"/> representing the asynchronous operation, and upon completion,
    /// returns the <see cref="DiscountDTO"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<DiscountDTO> GetByIdAsync(Guid Id);

    /// <summary>
    /// Asynchronously updates an existing discount in the repository.
    /// </summary>
    /// <param name="updatedDiscount">A <see cref="DiscountDTO"/> containing the updated data for the discount.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateAsync(DiscountDTO updatedDiscount);

    /// <summary>
    /// Asynchronously deletes a discount from the repository by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the discount to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteAsync(Guid Id);

    /// <summary>
    /// Asynchronously checks if a discount with the specified ID exists in the repository.
    /// </summary>
    /// <param name="Id">The unique identifier of the discount to check.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion,
    /// returns <c>true</c> if a discount with the given ID exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsAsync(Guid Id);

    /// <summary>
    /// Asynchronously retrieves the highest active discount for a specific hotel room type.
    /// </summary>
    /// <param name="hotelId">The unique identifier of the hotel.</param>
    /// <param name="type">The room type.</param>
    /// <returns>A <see cref="Task{DiscountDTO}"/> representing the asynchronous operation, and upon completion,
    /// returns the <see cref="DiscountDTO"/> representing the highest active discount for the specified hotel room type,
    /// or <c>null</c> if no active discount is found.
    /// </returns>
    Task<DiscountDTO> GetHighestDiscountActiveForHotelRoomTypeAsync(Guid hotelId, RoomType type);

    /// <summary>
    /// Asynchronously searches for discounts in the repository based on a predicate, with pagination and optional sorting,
    /// for admin users.
    /// </summary>
    /// <param name="expression">An <see cref="Expression{Func{Discount, bool}}"/> that defines the search conditions
    /// for discounts.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of discounts per page.</param>
    /// <param name="orderByDelegate">An optional <see cref="Func{IQueryable{Discount}, IOrderedQueryable{Discount}}"/> delegate to specify the sorting order.</param>
    /// <returns>A <see cref="Task{IEnumerable{DiscountForAdminResponseDTO}}"/> representing the asynchronous operation,
    /// and upon completion, returns a collection of <see cref="DiscountForAdminResponseDTO"/> that match the search criteria,
    /// tailored for admin responses.
    /// </returns>
    Task<IEnumerable<DiscountForAdminResponseDTO>> SearchForAdminAsync(
        Expression<Func<Discount, bool>> expression,
        int pageNumber,
        int pageSize,
        Func<IQueryable<Discount>, IOrderedQueryable<Discount>> orderByDelegate = null);

    /// <summary>
    /// Asynchronously retrieves all active discounts for a specific hotel.
    /// </summary>
    /// <param name="hotelId">The unique identifier of the hotel.</param>
    /// <returns>A <see cref="Task{IEnumerable{DiscountDTO}}"/> representing the asynchronous operation,
    /// and upon completion, returns a collection of <see cref="DiscountDTO"/>
    /// representing active discounts for the specified hotel.</returns>
    Task<IEnumerable<DiscountDTO>> GetActiveDiscountsForHotelAsync(Guid hotelId);
}