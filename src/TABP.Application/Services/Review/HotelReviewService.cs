using FluentValidation;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Models.HotelReview;
using TABP.Domain.Abstractions.Repositories.Review;
using TABP.Domain.Entities;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Abstractions.Services.Review;
using TABP.Domain.Exceptions;
using TABP.Domain.Abstractions.Utilities.Injectable;

namespace TABP.Application.Services.Review;

public class HotelReviewService : IHotelReviewService
{
    private readonly IHotelReviewRepository _hotelReviewRepository;
    private readonly IValidator<HotelReviewDTO> _reviewValidator;
    private readonly ICurrentUserService _currentUserService;
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;

    public HotelReviewService(
        IHotelReviewRepository hotelReviewRepository,
        IValidator<HotelReviewDTO> reviewValidator,
        ICurrentUserService currentUserService,
        IHotelRepository hotelRepository,
        IUnitOfWork unitOfWork)
    {
        _hotelReviewRepository = hotelReviewRepository;
        _reviewValidator = reviewValidator;
        _currentUserService = currentUserService;
        _hotelRepository = hotelRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddAsync(HotelReviewDTO newReview)
    {
        await _reviewValidator
            .ValidateAndThrowAsync(newReview);

        newReview.UserId = _currentUserService
            .GetUserId();

        await _hotelReviewRepository
            .AddAsync(newReview);

        await UpdateHotelStarRatingAsync(
            newReview.HotelId);

        await _unitOfWork
            .SaveChangesAsync();
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
           persistedReview.HotelId);

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

    public async Task<decimal> GetReviewsByHotelCountAsync(Guid hotelId) =>
        await _hotelReviewRepository.GetReviewsByHotelCountAsync(hotelId);

    public async Task<IEnumerable<HotelReview>> GetReviewsByUserAsync(Guid userId) =>
        await _hotelReviewRepository.GetReviewsByUserAsync(userId);

    public async Task UpdateAsync(HotelReviewDTO updatedReview)
    {
        await _reviewValidator.ValidateAndThrowAsync(updatedReview);
        await ValidateId(updatedReview.Id);

        await ValidateOwnership(
            updatedReview.Id,
            _currentUserService.GetUserId()); // maybe move to fluentvalidation?

        updatedReview.ModificationDate = DateTime.UtcNow;

        await _hotelReviewRepository
            .UpdateAsync(updatedReview);

        await UpdateHotelStarRatingAsync(
            updatedReview.HotelId);

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task ValidateId(Guid Id)
    {
        if (!await ExistsAsync(Id))
        {
            throw new EntityNotFoundException($"Id {Id} Does not exist.");
        }
    }

    private async Task ValidateOwnership(Guid reviewId, Guid currentUserId)
    {
        if (!await ExistsAsync(reviewId, currentUserId))
        {
            throw new EntityNotFoundException($"Review with Id {reviewId} does not exist for the current user.");
        }
    }

    private async Task UpdateHotelStarRatingAsync(Guid hotelId)
    {
        var hotel = await _hotelRepository.GetByIdAsync(hotelId);
        var averageRating = await _hotelReviewRepository
            .GetAverageRatingByHotelAsync(hotelId);
        hotel.StarRating = Convert.ToDecimal(averageRating);
        _hotelRepository.Update(hotel);
    }
}