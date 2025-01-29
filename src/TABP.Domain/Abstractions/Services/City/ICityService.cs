using TABP.Domain.Models.City;

namespace TABP.Domain.Abstractions.Services.City;


public interface ICityService
{
    Task<CityDTO> GetByIdAsync(Guid Id);
    Task AddAsync(CityDTO newCity);
    Task DeleteAsync(Guid Id);
    Task UpdateAsync(CityDTO updatedCity);
    Task<bool> ExistsAsync(Guid Id);
}