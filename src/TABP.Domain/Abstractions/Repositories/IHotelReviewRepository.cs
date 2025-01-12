using TABP.Domain.Entities;

namespace TABP.Domain.Abstractions.Repositories;

public interface IHotelReviewRepository
{
    Task<HotelReview> AddAsync(HotelReview newReview);

    Task<HotelReview?> GetByIdAsync(Guid reviewId);

    Task<decimal> GetReviewsByHotelCount(Guid hotelId);

    Task<HotelReview?> ExistsByUserAndHotel(Guid userId, Guid HotelId);   
}