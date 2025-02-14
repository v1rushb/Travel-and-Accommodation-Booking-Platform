using TABP.Domain.Models.Cart.Search;
using TABP.Domain.Models.Cart.Search.Response;
using TABP.Domain.Models.Cart.Sort;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services.Cart;

/// <summary>
/// Defines administrative operations for managing shopping carts.
/// This interface outlines methods for searching carts in an administrative context,
/// allowing for filtering, pagination, and sorting to manage cart data effectively.
/// </summary>s
public interface ICartAdminService
{
    /// <summary>
    /// Searches for shopping carts based on the specified criteria, tailored for administrative use.
    /// This method supports filtering, pagination, and sorting to retrieve a list of carts
    /// that match the given search parameters. It is designed for administrative interfaces
    /// where detailed management of shopping cart records is required, such as reviewing abandoned carts or order history.
    /// </summary>
    /// <param name="pagination">
    /// The pagination parameters that control the result set size and page number.
    /// Used to manage large datasets by dividing results into pages, improving performance
    /// and user experience when dealing with numerous cart records.
    /// </param>
    /// <param name="query">
    /// The cart search query containing filtering options.
    /// This parameter allows for specifying criteria to narrow down the search results,
    /// such as user ID, cart status, or date ranges.
    /// </param>
    /// <param name="sortQuery">
    /// The sorting parameters that determine the order of the results.
    /// Allows administrators to sort carts based on different fields such as creation date, total price, etc.,
    /// in ascending or descending order to facilitate data review and management.
    /// </param>
    /// <returns>
    /// A collection of <see cref="CartAdminResponseDTO"/> representing shopping carts that match the search criteria.
    /// Each <see cref="CartAdminResponseDTO"/> object contains detailed information about a cart,
    /// suitable for display in administrative interfaces, including cart items, user details, and total price.
    /// </returns>
    Task<IEnumerable<CartAdminResponseDTO>> SearchAsync(
        PaginationDTO pagination,
        CartSearchQuery query,
        CartSortQuery sortQuery);
}