using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Sorting.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services.Cart;
using TABP.Domain.Abstractions.Utilities.Injectable;
using TABP.Domain.Models.Cart.Search;
using TABP.Domain.Models.Cart.Search.Response;
using TABP.Domain.Models.Cart.Sort;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services.Cart;

public class CartAdminService : ICartAdminService
{
    private readonly ICartRepository _cartRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly ILogger<CartAdminService> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<CartSortQuery> _cartSortQueryValidator;

    public CartAdminService(
        ICartRepository cartRepository,
        IValidator<PaginationDTO> paginationValidator,
        ILogger<CartAdminService> logger,
        ICurrentUserService currentUserService,
        IValidator<CartSortQuery> cartSortQueryValidator)
    {
        _cartRepository = cartRepository;
        _paginationValidator = paginationValidator;
        _logger = logger;
        _currentUserService = currentUserService;
        _cartSortQueryValidator = cartSortQueryValidator;
    }
    public async Task<IEnumerable<CartAdminResponseDTO>> SearchAsync(
        PaginationDTO pagination,
        CartSearchQuery query,
        CartSortQuery sortQuery)
    {
        await _paginationValidator.ValidateAndThrowAsync(pagination);

        sortQuery.IsAdmin = true;
        await _cartSortQueryValidator.ValidateAndThrowAsync(sortQuery);

        var filterExpression = CartExpressionBuilder.Build(query);
        var orderByDelegate = CartSortExpressionBuilder
            .GetSortDelegate(sortQuery);

        _logger.LogInformation(
            @"
                Searching For Carts with query {CartSearchQuery}
                Sorting: {CartSortQuery}
                PageNumber: {PageNumber}
                PageSize: {PageSize}
                By User With Id: {UserId}",

                query,
                sortQuery,
                pagination.PageNumber,
                pagination.PageSize,
                _currentUserService.GetUserId()
            );

        return await _cartRepository.SearchAdminAsync(
            filterExpression,
            pagination.PageNumber,
            pagination.PageSize,
            orderByDelegate
        );
    }
}