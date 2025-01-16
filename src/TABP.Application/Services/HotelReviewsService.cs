using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Entities;
using TABP.Domain.Models.HotelReview;

namespace TABP.Application.Services;

public class HotelReviewService : IHotelReviewService
{
    private readonly IHotelReviewRepository _hotelReviewRepository;
    private readonly ILogger<HotelReviewService> _logger;
    
    public HotelReviewService(
        IHotelReviewRepository hotelReviewRepository,
        ILogger<HotelReviewService> logger)
    {
        _hotelReviewRepository = hotelReviewRepository;
        _logger = logger;
    }

    public async Task<Guid> AddAsync(HotelReviewDTO newReview)
    {
        // some validations.

        var reviewId = await _hotelReviewRepository.AddAsync(newReview);

        return reviewId;
    }

    public async Task DeleteAsync(Guid Id)
    {
        await ValidateId(Id);

        await _hotelReviewRepository.DeleteAsync(Id);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _hotelReviewRepository.ExistsAsync(Id);

    public async Task<bool> ExistsByUserAndHotelAsync(Guid userId, Guid hotelId) =>
        await _hotelReviewRepository.ExistsByUserAndHotelAsync(userId, hotelId);

    public async Task<double> GetAverageRatingByHotelAsync(Guid hotelId) =>
        await _hotelReviewRepository.GetAverageRatingByHotelAsync(hotelId);

    public async Task<HotelReview> GetByIdAsync(Guid Id)
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
}