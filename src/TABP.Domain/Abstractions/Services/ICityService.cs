using TABP.Domain.Models.City;

namespace TABP.Domain.Abstractions.Services;

public interface ICityService
{
    Task<CityDTO?> GetByIdAsync(Guid Id);
    Task<Guid> AddAsync(CityDTO newCity);
    Task DeleteAsync(Guid Id);
    Task UpdateAsync(CityDTO updatedCity);
    Task<bool> ExistsAsync(Guid Id);
}