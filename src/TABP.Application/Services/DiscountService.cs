using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Exceptions;
using TABP.Domain.Models.Discount;
using TABP.Domain.Models.Discount.Search;
using TABP.Domain.Models.Discount.Search.Response;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _discountRepository;
    private readonly ILogger<DiscountService> _logger;
    private readonly IValidator<DiscountDTO> _discountValidator;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly ICurrentUserService _currentUserService;

    public DiscountService(
       IDiscountRepository discountRepository,
       ILogger<DiscountService> logger,
       IValidator<DiscountDTO> discountValidator,
       IValidator<PaginationDTO> paginationValidator,
       ICurrentUserService currentUserService)
    {
        _discountRepository = discountRepository;
        _logger = logger;
        _discountValidator = discountValidator;
        _paginationValidator = paginationValidator;
        _currentUserService = currentUserService;
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
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var expression = DiscountForAdminExpressionBuilder.Build(query);

        _logger.LogInformation(
            "Searching for Discounts with query {@DiscountSearchQuery} by User {UserId}",
            query,
            _currentUserService.GetUserId());

        return await _discountRepository.SearchForAdminAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);
    }

    public async Task<IEnumerable<DiscountDTO>> GetActiveDiscountsForHotelAsync(Guid hotelId) =>
        await _discountRepository.GetActiveDiscountsForHotelAsync(hotelId);
}