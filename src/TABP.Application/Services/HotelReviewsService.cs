using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel;
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
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;

    public HotelReviewService(
        IHotelReviewRepository hotelReviewRepository,
        ILogger<HotelReviewService> logger,
        IValidator<HotelReviewDTO> reviewValidator,
        ICurrentUserService currentUserService,
        IValidator<PaginationDTO> paginationValidator,
        IHotelRepository hotelRepository,
        IUnitOfWork unitOfWork)
    {
        _hotelReviewRepository = hotelReviewRepository;
        _logger = logger;
        _reviewValidator = reviewValidator;
        _currentUserService = currentUserService;
        _paginationValidator = paginationValidator;
        _hotelRepository = hotelRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> AddAsync(HotelReviewDTO newReview)
    {
        await _reviewValidator
            .ValidateAndThrowAsync(newReview);

        newReview.UserId = _currentUserService
            .GetUserId();

        var reviewId = await _hotelReviewRepository
            .AddAsync(newReview);

        await UpdateHotelStarRatingAsync(
            newReview.HotelId, 
            newRating: newReview.Rating);

        await _unitOfWork
            .SaveChangesAsync();
        
        return reviewId;
    }

    public async Task DeleteAsync(Guid Id)
    {
        await ValidateId(Id);
        await ValidateOwnership(
            Id,
            _currentUserService.GetUserId());

        var persistedReview = await _hotelReviewRepository
            .GetByIdAsync(Id);

        await _hotelReviewRepository
            .DeleteAsync(Id);

         await UpdateHotelStarRatingAsync(
            persistedReview.HotelId, 
            originalRating: persistedReview.Rating);
    
        await _unitOfWork.SaveChangesAsync();
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
        
        await ValidateOwnership(
            updatedReview.Id,
            _currentUserService.GetUserId()); // maybe move to fluentvalidation?

        updatedReview.ModificationDate = DateTime.UtcNow;

        var persistedReview = await _hotelReviewRepository
            .GetByIdAsync(updatedReview.Id);

        await _hotelReviewRepository
            .UpdateAsync(updatedReview);

        await UpdateHotelStarRatingAsync(
            updatedReview.HotelId, 
            newRating: updatedReview.Rating,
            originalRating: persistedReview.Rating);

        await _unitOfWork.SaveChangesAsync();
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

    public async Task<IEnumerable<HotelReviewAdminResponseDTO>> SearchForAdminAsync(
        AdminReviewSearchQuery inQuery,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var expression = ReviewForAdminExpressionBuilder.Build(inQuery);
        return await _hotelReviewRepository.SearchReviewsForAdminAsync( 
            expression,
            pagination.PageNumber,
            pagination.PageSize);
    }

    private async Task UpdateHotelStarRatingAsync(
        Guid hotelId, 
        decimal? newRating = null, 
        decimal? originalRating = null)
    {
        var hotel = await _hotelRepository.GetByIdAsync(hotelId);
        
        if (!newRating.HasValue && originalRating.HasValue)
        {
            hotel.StarRating -= originalRating.Value;
            return;
        }

        if (newRating.HasValue && originalRating.HasValue)
        {
            hotel.StarRating += newRating.Value - originalRating.Value;
            return;
        }

        if (newRating.HasValue)
        {
            hotel.StarRating += newRating.Value;
        }
    }
}