using TABP.Domain.Models.Cart.Search;
using TABP.Domain.Models.Cart.Search.Response;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services.Cart;

public interface ICartAdminService
{
    Task<IEnumerable<CartAdminResponseDTO>> SearchAsync(PaginationDTO pagination, CartSearchQuery query);
}