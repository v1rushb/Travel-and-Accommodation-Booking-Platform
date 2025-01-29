using FluentValidation;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services.Cart;
using TABP.Domain.Models.Cart.Search;
using TABP.Domain.Models.Cart.Search.Response;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services.Cart;

public class CartAdminService : ICartAdminService
{
    private readonly ICartRepository _cartRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;

    public CartAdminService(
        ICartRepository cartRepository,
        IValidator<PaginationDTO> paginationValidator)
    {
        _cartRepository = cartRepository;
        _paginationValidator = paginationValidator;
    }
    public async Task<IEnumerable<CartAdminResponseDTO>> SearchAsync(
        PaginationDTO pagination,
        CartSearchQuery query)
    {
        var predicate = CartExpressionBuilder.Build(query);
        return await _cartRepository.SearchAdminAsync(
            predicate,
            pagination.PageNumber,
            pagination.PageSize);
    }
}