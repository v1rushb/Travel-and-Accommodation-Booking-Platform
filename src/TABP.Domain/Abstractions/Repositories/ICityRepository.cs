using TABP.Domain.Entities;
using TABP.Domain.Models.City;

namespace TABP.Domain.Abstractions.Repositories;

public interface ICityRepository
{
    Task<Guid> AddAsync(CityDTO newCity);
    Task<bool> ExistsAsync(Guid Id);
    Task<CityDTO?> GetByIdAsync(Guid Id);
    Task DeleteAsync(Guid Id);
    Task UpdateAsync(CityDTO updatedCity);
}