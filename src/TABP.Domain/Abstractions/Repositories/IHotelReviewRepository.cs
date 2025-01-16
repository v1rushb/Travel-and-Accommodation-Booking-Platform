using TABP.Domain.Entities;
using TABP.Domain.Models.HotelReview;

namespace TABP.Domain.Abstractions.Repositories;

public interface IHotelReviewRepository
{
    Task<Guid> AddAsync(HotelReviewDTO newReview);

    Task<HotelReview?> GetByIdAsync(Guid reviewId);

    Task<decimal> GetReviewsByHotelCountAsync(Guid hotelId);

    Task<bool> ExistsByUserAndHotelAsync(Guid userId, Guid HotelId);

    Task UpdateAsync(HotelReviewDTO updatedReview);
    Task DeleteAsync(Guid reviewId);
}