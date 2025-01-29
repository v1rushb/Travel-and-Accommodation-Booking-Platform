using TABP.Domain.Entities;
using TABP.Domain.Models.HotelReview;

namespace TABP.Domain.Abstractions.Services.Review;

public interface IHotelReviewService
{
    Task AddAsync(HotelReviewDTO newReview);
    Task<HotelReviewDTO> GetByIdAsync(Guid reviewId);
    Task UpdateAsync(HotelReviewDTO updatedReview);
    Task DeleteAsync(Guid reviewId);
    Task<bool> ExistsAsync(Guid reviewId, Guid? userId = null);
    Task<bool> ExistsByUserAndHotelAsync(Guid userId, Guid HotelId);

    Task<decimal> GetReviewsByHotelCountAsync(Guid hotelId);
    Task<double> GetAverageRatingByHotelAsync(Guid hotelId);

    // Task<IEnumerable<HotelReview>> GetReviewsByHotelAsync(Guid hotelId);
    Task<IEnumerable<HotelReview>> GetReviewsByUserAsync(Guid userId);
    // Task<HotelReview?> GetByUserAndHotelAsync(Guid userId, Guid hotelId);
}