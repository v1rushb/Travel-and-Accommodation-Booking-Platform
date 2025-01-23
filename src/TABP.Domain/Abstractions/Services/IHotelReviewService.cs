using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.HotelReview;
using TABP.Domain.Models.HotelReview.Search;
using TABP.Domain.Models.HotelReview.Search.Response;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services;

public interface IHotelReviewService
{
    Task<Guid> AddAsync(HotelReviewDTO newReview);
    Task<HotelReviewDTO> GetByIdAsync(Guid reviewId);
    Task UpdateAsync(HotelReviewDTO updatedReview);
    Task DeleteAsync(Guid reviewId);

    Task<bool> ExistsAsync(Guid reviewId, Guid? userId = null); 
    Task<bool> ExistsByUserAndHotelAsync(Guid userId, Guid HotelId);
    Task<decimal> GetReviewsByHotelCountAsync(Guid hotelId);
    Task<double> GetAverageRatingByHotelAsync(Guid hotelId);

    Task<IEnumerable<HotelReview>> GetReviewsByHotelAsync(Guid hotelId);
    Task<IEnumerable<HotelReview>> GetReviewsByUserAsync(Guid userId);
    Task<HotelReview?> GetByUserAndHotelAsync(Guid userId, Guid hotelId);
    Task<IEnumerable<HotelReviewUserResponseDTO>> SearchReviewsAsync(ReviewSearchQuery query, PaginationDTO pagination);
    Task<IEnumerable<HotelReviewAdminResponseDTO>> SearchForAdminAsync(AdminReviewSearchQuery query, PaginationDTO pagination);
}