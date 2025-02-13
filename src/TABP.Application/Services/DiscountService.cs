using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Sorting.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Abstractions.Utilities.Injectable;
using TABP.Domain.Exceptions;
using TABP.Domain.Models.Discount;
using TABP.Domain.Models.Discount.Search;
using TABP.Domain.Models.Discount.Search.Response;
using TABP.Domain.Models.Discount.Sort;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _discountRepository;
    private readonly ILogger<DiscountService> _logger;
    private readonly IValidator<DiscountDTO> _discountValidator;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<DiscountSortQuery> _discountSortQueryValidator;

    public DiscountService(
       IDiscountRepository discountRepository,
       ILogger<DiscountService> logger,
       IValidator<DiscountDTO> discountValidator,
       IValidator<PaginationDTO> paginationValidator,
       ICurrentUserService currentUserService,
       IValidator<DiscountSortQuery> discountSortQueryValidator)
    {
        _discountRepository = discountRepository;
        _logger = logger;
        _discountValidator = discountValidator;
        _paginationValidator = paginationValidator;
        _currentUserService = currentUserService;
        _discountSortQueryValidator = discountSortQueryValidator;
    }
    public async Task AddAsync(DiscountDTO newDiscount)
    {
        await _discountValidator.ValidateAndThrowAsync(newDiscount);

        await _discountRepository.AddAsync(newDiscount);
    }

    public async Task DeleteAsync(Guid Id)
    {
        await ValidateId(Id);

        await _discountRepository.DeleteAsync(Id);
    }

    public async Task<DiscountDTO> GetByIdAsync(Guid Id)
    {
        await ValidateId(Id);

        return await _discountRepository.GetByIdAsync(Id);
    }

    public async Task UpdateAsync(DiscountDTO updatedDiscount)
    {
        await _discountValidator.ValidateAndThrowAsync(updatedDiscount);
        await ValidateId(updatedDiscount.Id);

        await _discountRepository.UpdateAsync(updatedDiscount);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _discountRepository.ExistsAsync(Id);
        
    private async Task ValidateId(Guid Id)
    {
        if(! await ExistsAsync(Id))
            throw new EntityNotFoundException($"Id {Id} Does not exist.");
    }

    public async Task<IEnumerable<DiscountForAdminResponseDTO>> SearchForAdminAsync(
        DiscountSearchQuery query,
        PaginationDTO pagination,
        DiscountSortQuery sortQuery)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        sortQuery.IsAdmin = true;
        _discountSortQueryValidator.ValidateAndThrow(sortQuery);

        var filterExpression = DiscountForAdminExpressionBuilder.Build(query);
        var orderByDelegate = DiscountSortExpressionBuilder
            .GetSortDelegate(sortQuery);

        _logger.LogInformation(
            @"
                Searching For Discounts with query {DiscountSearchQuery}
                Sorting: {DiscountSortQuery}
                PageNumber: {PageNumber}
                PageSize: {PageSize}
                By User With Id: {UserId}",

                query,
                sortQuery,
                pagination.PageNumber,
                pagination.PageSize, 
                _currentUserService.GetUserId()
            );

        return await _discountRepository.SearchForAdminAsync(
            filterExpression,
            pagination.PageNumber,
            pagination.PageSize,
            orderByDelegate
        );
    }

    public async Task<IEnumerable<DiscountDTO>> GetActiveDiscountsForHotelAsync(Guid hotelId) =>
        await _discountRepository.GetActiveDiscountsForHotelAsync(hotelId);
}