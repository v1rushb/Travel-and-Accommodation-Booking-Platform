using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Entities;
using TABP.Domain.Models.HotelReview;
using TABP.Domain.Models.HotelReview.Search;
using TABP.Domain.Models.HotelReview.Search.Response;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services;

public class HotelReviewService : IHotelReviewService
{
    private readonly IHotelReviewRepository _hotelReviewRepository;
    private readonly ILogger<HotelReviewService> _logger;
    private readonly IValidator<HotelReviewDTO> _reviewValidator;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    
    public HotelReviewService(
        IHotelReviewRepository hotelReviewRepository,
        ILogger<HotelReviewService> logger,
        IValidator<HotelReviewDTO> reviewValidator,
        ICurrentUserService currentUserService,
        IValidator<PaginationDTO> paginationValidator)
    {
        _hotelReviewRepository = hotelReviewRepository;
        _logger = logger;
        _reviewValidator = reviewValidator;
        _currentUserService = currentUserService;
        _paginationValidator = paginationValidator;
    }

    public async Task<Guid> AddAsync(HotelReviewDTO newReview)
    {
        var currentUserId = _currentUserService.GetUserId();
        await _reviewValidator.ValidateAndThrowAsync(newReview);

        newReview.UserId = currentUserId;

        var reviewId = await _hotelReviewRepository.AddAsync(newReview);

        return reviewId;
    }

    public async Task DeleteAsync(Guid Id)
    {
        await ValidateId(Id);
        await ValidateOwnership(Id, _currentUserService.GetUserId());

        await _hotelReviewRepository.DeleteAsync(Id);
    }

    public async Task<bool> ExistsAsync(Guid Id, Guid? userId = null) =>
        await _hotelReviewRepository.ExistsAsync(Id, userId);

    public async Task<bool> ExistsByUserAndHotelAsync(Guid userId, Guid hotelId) =>
        await _hotelReviewRepository.ExistsByUserAndHotelAsync(userId, hotelId);

    public async Task<double> GetAverageRatingByHotelAsync(Guid hotelId) =>
        await _hotelReviewRepository.GetAverageRatingByHotelAsync(hotelId);

    public async Task<HotelReviewDTO> GetByIdAsync(Guid Id)
    {
        await ValidateId(Id);

        return await _hotelReviewRepository.GetByIdAsync(Id);
    }

    public async Task<IEnumerable<HotelReview>> GetReviewsByHotelAsync(Guid hotelId) =>
        await _hotelReviewRepository.GetReviewsByHotelAsync(hotelId);

    public async Task<decimal> GetReviewsByHotelCountAsync(Guid hotelId) =>
        await _hotelReviewRepository.GetReviewsByHotelCountAsync(hotelId);

    public async Task<IEnumerable<HotelReview>> GetReviewsByUserAsync(Guid userId) =>
        await _hotelReviewRepository.GetReviewsByUserAsync(userId);

    public async Task UpdateAsync(HotelReviewDTO updatedReview)
    {
        await ValidateId(updatedReview.Id);
        var currentUserId = _currentUserService.GetUserId();
        
        await ValidateOwnership(updatedReview.Id, currentUserId); // maybe move to fluentvalidation?

        updatedReview.ModificationDate = DateTime.UtcNow;

        await _hotelReviewRepository.UpdateAsync(updatedReview);
    }
    
    private async Task ValidateId(Guid Id)
    {
        if(! await ExistsAsync(Id))
        {
            throw new KeyNotFoundException($"Id {Id} Does not exist.");
        }
    }

    private async Task ValidateOwnership(Guid reviewId, Guid currentUserId)
    {
        if(! await ExistsAsync(reviewId, currentUserId))
        {
            throw new KeyNotFoundException($"Review with Id {reviewId} does not exist for the current user.");
        }
    }

    public async Task<HotelReview?> GetByUserAndHotelAsync(Guid userId, Guid hotelId) {
        await ValidateId(hotelId);

        return await _hotelReviewRepository.GetByUserAndHotelAsync(userId, hotelId);
    }

    public async Task<IEnumerable<HotelReviewUserResponseDTO>> SearchReviewsAsync(
        ReviewSearchQuery query,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var currentUserId = _currentUserService.GetUserId();
        var expression = ReviewExpressionBuilder.Build(query, currentUserId);

        return await _hotelReviewRepository.SearchReviewsAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);
    }
}