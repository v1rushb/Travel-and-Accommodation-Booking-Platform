using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Abstractions.Services;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
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

    public DiscountService(
       IDiscountRepository discountRepository,
       ILogger<DiscountService> logger,
       IValidator<DiscountDTO> discountValidator)
    {
        _discountRepository = discountRepository;
        _logger = logger;
        _discountValidator = discountValidator;
    }
    public async Task<Guid> AddAsync(DiscountDTO newDiscount)
    {
        await _discountValidator.ValidateAndThrowAsync(newDiscount);

        return await _discountRepository.AddAsync(newDiscount);
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
        await ValidateId(updatedDiscount.Id);

        await _discountRepository.UpdateAsync(updatedDiscount);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _discountRepository.ExistsAsync(Id);
        
    private async Task ValidateId(Guid Id)
    {
        if(! await ExistsAsync(Id))
            throw new KeyNotFoundException($"Id {Id} Does not exist.");
    }

    public async Task<IEnumerable<Discount>> GetByHotelAsync(Guid hotelId) => // do some validations?
        await _discountRepository.GetByHotelAsync(hotelId);

    public async Task<IEnumerable<DiscountForAdminResponseDTO>> SearchForAdminAsync(
        DiscountSearchQuery query, PaginationDTO pagination)
    {
        var expression = DiscountForAdminExpressionBuilder.Build(query);
        return await _discountRepository.SearchForAdminAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);
    }

}