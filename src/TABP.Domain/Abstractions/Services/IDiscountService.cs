using TABP.Domain.Models.Discount;
using TABP.Domain.Models.Discount.Search;
using TABP.Domain.Models.Discount.Search.Response;
using TABP.Domain.Models.Discount.Sort;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services;

/// <summary>
/// Defines discount management operations.
/// </summary>
public interface IDiscountService
{
    /// <summary>
    /// Retrieves a discount by its unique<see cref="Guid"/>identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the discount.</param>
    /// <returns>Discount DTO containing required details.</returns>
    Task<DiscountDTO> GetByIdAsync(Guid Id);

    /// <summary>
    /// Adds a new discount.
    /// </summary>
    /// <param name="newDiscount">A Discount DTO containing discount details to be added.</param>
    Task AddAsync(DiscountDTO newDiscount);

    /// <summary>
    /// Updates an existing discount.
    /// </summary>
    /// <param name="updatedDiscount">A Discount DTO containing discount details to be updated.</param>
    Task UpdateAsync(DiscountDTO updatedDiscount);

    /// <summary>
    /// Deletes a discount by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique<see cref="Guid"/>identifier of the discount to be deleted.</param>
    Task DeleteAsync(Guid Id);

    /// <summary>
    /// Checks if a discount exists by its unique<see cref="Guid"/>identifier.
    /// </summary>
    /// <param name="Id">The unique<see cref="Guid"/>identifier of the discount.</param>
    /// <returns><c>true</c> if the discount exists; otherwise, <c>false</c>.</returns>
    Task<bool> ExistsAsync(Guid Id);

    /// <summary>
    /// Admin Search for discounts based on the provided search, pagination, and sorting criteria.
    /// If no options provided defaults are to be considered.
    /// </summary>
    /// <param name="query">Search and Filteration parameters to control result set.</param>
    /// <param name="pagination">Pagination parameters to control the result set.</param>
    /// <param name="sortQuery">Sorting parameters to control the result set.</param>
    /// <returns>
    /// A collection of filtered, sorted and paginated discounts
    ///formatted for administrative responses<see cref="DiscountForAdminResponseDTO"/>.
    /// </returns>
    /// <exception cref="FluentValidation.ValidationException">
    /// Thrown when the provided sorting parameters are invalid.
    /// </exception>
    Task<IEnumerable<DiscountForAdminResponseDTO>> SearchForAdminAsync(
        DiscountSearchQuery query,
        PaginationDTO pagination,
        DiscountSortQuery sortQuery);

    
    /// <summary>
    /// Retrieves active discounts for a specified hotel.
    /// </summary>
    /// <param name="hotelId">The unique<see cref="Guid"/>identifier of the hotel.</param>
    /// <returns>
    /// A collection of <see cref="DiscountDTO"/> representing active discounts.
    Task<IEnumerable<DiscountDTO>> GetActiveDiscountsForHotelAsync(Guid hotelId);
}