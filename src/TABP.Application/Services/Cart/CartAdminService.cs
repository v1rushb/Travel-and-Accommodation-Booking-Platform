using FluentValidation;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<CartAdminService> _logger;
    private readonly ICurrentUserService _currentUserService;

    public CartAdminService(
        ICartRepository cartRepository,
        IValidator<PaginationDTO> paginationValidator,
        ILogger<CartAdminService> logger,
        ICurrentUserService currentUserService)
    {
        _cartRepository = cartRepository;
        _paginationValidator = paginationValidator;
        _logger = logger;
        _currentUserService = currentUserService;
    }
    public async Task<IEnumerable<CartAdminResponseDTO>> SearchAsync(
        PaginationDTO pagination,
        CartSearchQuery query)
    {
        await _paginationValidator.ValidateAndThrowAsync(pagination);

        var predicate = CartExpressionBuilder.Build(query);

        _logger.LogInformation(
            "Searching for Carts with query {@CartSearchQuery} by User {UserId}", 
            query, 
            _currentUserService.GetUserId());

        return await _cartRepository.SearchAdminAsync(
            predicate,
            pagination.PageNumber,
            pagination.PageSize);
    }
}