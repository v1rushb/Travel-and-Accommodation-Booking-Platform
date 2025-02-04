using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.HotelReview;

namespace TABP.Domain.Abstractions.Repositories.Review;

public interface IHotelReviewRepository
{
    Task<Guid> AddAsync(HotelReviewDTO newReview);

    Task<HotelReviewDTO> GetByIdAsync(Guid reviewId);

    Task<decimal> GetReviewsByHotelCountAsync(Guid hotelId);

    Task<bool> ExistsByUserAndHotelAsync(Guid userId, Guid HotelId);

    Task UpdateAsync(HotelReviewDTO updatedReview);
    Task DeleteAsync(Guid reviewId);
    Task<double> GetAverageRatingByHotelAsync(Guid hotelId);
    // Task<IEnumerable<HotelReview>> GetReviewsByHotelAsync(Guid hotelId);
    Task<IEnumerable<HotelReview>> GetReviewsByUserAsync(Guid userId);
    Task<bool> ExistsAsync(Guid reviewId, Guid? userId = null);
    // Task<HotelReview?> GetByUserAndHotelAsync(Guid userId, Guid hotelId);
    Task<IEnumerable<HotelReviewDTO>> SearchAsync(
        Expression<Func<HotelReview, bool>> predicate,
        int pageNumber,
        int pageSize,
        Func<IQueryable<HotelReview>, IOrderedQueryable<HotelReview>> orderByDelegate = null);
}