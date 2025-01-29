using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.Hotel.Search.Response;

namespace TABP.Domain.Abstractions.Repositories;

public interface IHotelUserRepository
{
    Task<HotelPageResponseDTO> GetHotelPageAsync(Guid hotelId);
    Task<IEnumerable<VisitedHotelDTO>> GetWeeklyFeaturedHotelsAsync(Expression<Func<Hotel, bool>> predicate);
    Task<IEnumerable<HotelHistoryDTO>> GetHotelHistoryAsync(Expression<Func<HotelVisit, bool>> predicate, Guid userId, int pageNumber, int pageSize);

}