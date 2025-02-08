using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.HotelVisit;

namespace TABP.Domain.Abstractions.Repositories;

public interface IHotelVisitRepository 
{
    Task<Guid> AddAsync(HotelVisitDTO newHotelVisit);
    Task<bool> ExistsAsync(Guid Id);
    // Task<IEnumerable<HotelVisit>> GetByUserAsync(Guid userId, DateTime? startDate = null, DateTime? endDate = null);
    // Task<IEnumerable<HotelVisit>> GetByHotelAsync(Guid hotelId, DateTime? startDate = null, DateTime? endDate = null);
    // Task<IEnumerable<HotelVisit>> GetByUserAndHotelAsync(Guid userId, Guid hotelId, DateTime? startDate = null, DateTime? endDate = null);
    Task<IEnumerable<VisitedHotelDTO>> GetVisitedHotels(
        Expression<Func<HotelVisit, bool>> predicate,
        int pageNumber,
        int pageSize);
}