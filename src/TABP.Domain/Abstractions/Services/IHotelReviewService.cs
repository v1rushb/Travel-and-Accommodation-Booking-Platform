using TABP.Domain.Entities;
using TABP.Domain.Models.HotelReview;

namespace TABP.Domain.Abstractions.Services;

public interface IHotelReviewService
{
    Task<Guid> AddAsync(HotelReviewDTO newReview);
    Task<HotelReview> GetByIdAsync(Guid reviewId);
    Task UpdateAsync(HotelReviewDTO updatedReview);
    Task DeleteAsync(Guid reviewId);
    Task<bool> ExistsByUserAndHotelAsync(Guid userId, Guid HotelId);
    Task<decimal> GetReviewsByHotelCountAsync(Guid hotelId);
}