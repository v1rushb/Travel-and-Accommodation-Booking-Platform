using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.HotelReview;
using TABP.Domain.Models.HotelReview.Search.Response;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Repositories;

public interface IHotelReviewRepository
{
    Task<Guid> AddAsync(HotelReviewDTO newReview);

    Task<HotelReviewDTO> GetByIdAsync(Guid reviewId);

    Task<decimal> GetReviewsByHotelCountAsync(Guid hotelId);

    Task<bool> ExistsByUserAndHotelAsync(Guid userId, Guid HotelId);

    Task UpdateAsync(HotelReviewDTO updatedReview);
    Task DeleteAsync(Guid reviewId);
    Task<double> GetAverageRatingByHotelAsync(Guid hotelId);
    Task<IEnumerable<HotelReview>> GetReviewsByHotelAsync(Guid hotelId);
    Task<IEnumerable<HotelReview>> GetReviewsByUserAsync(Guid userId);
    Task<bool> ExistsAsync(Guid reviewId, Guid? userId = null);
    Task<HotelReview?> GetByUserAndHotelAsync(Guid userId, Guid hotelId);
    Task<IEnumerable<HotelReviewUserResponseDTO>> SearchReviewsAsync(Expression<Func<HotelReview, bool>> predicate, int pageNumber, int pageSize);
}