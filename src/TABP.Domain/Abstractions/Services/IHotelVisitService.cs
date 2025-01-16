using TABP.Domain.Entities;
using TABP.Domain.Models.HotelVisit;

namespace TABP.Domain.Abstractions.Services;

public interface IHotelVisitService
{
    Task<Guid> AddAsync(HotelVisitDTO newHotelVisit);
    Task<bool> ExistsAsync(Guid Id);
    Task UpdateAsync(HotelVisitDTO updatedHotelVisit);
    Task DeleteAsync(Guid Id);
    Task<HotelVisit> GetByIdAsync(Guid Id);
    Task<IEnumerable<HotelVisit>> GetByUserAsync(Guid userId);
    Task<IEnumerable<HotelVisit>> GetByHotelAsync(Guid HotelId);
}