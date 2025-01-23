using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.Hotels;
using TABP.Domain.Models.HotelVisit;

namespace TABP.Domain.Abstractions.Services;

public interface IHotelVisitService
{
    Task<Guid> AddAsync(HotelVisitDTO newHotelVisit);
    Task<bool> ExistsAsync(Guid Id);
    Task<IEnumerable<HotelVisit>> GetByUserAsync(Guid userId, DateTime? startDate = null, DateTime? endDate = null);
    Task<IEnumerable<HotelVisit>> GetByHotelAsync(Guid HotelId, DateTime? startDate = null, DateTime? endDate = null);
    Task<IEnumerable<HotelVisit>> GetByUserAndHotelAsync(Guid userId, Guid hotelId, DateTime? startDate = null, DateTime? endDate = null);
    Task<IEnumerable<VisitedHotelDTO>> GetTop5VisitedHotels(VisitTimeOptionQuery query);
}