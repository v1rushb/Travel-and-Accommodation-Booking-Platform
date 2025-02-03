using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.HotelVisit;

namespace TABP.Domain.Abstractions.Services;

public interface IHotelVisitService
{
    Task AddAsync(HotelVisitDTO newHotelVisit);
    Task<IEnumerable<VisitedHotelDTO>> GetTop5VisitedHotels(VisitTimeOptionQuery query);
}