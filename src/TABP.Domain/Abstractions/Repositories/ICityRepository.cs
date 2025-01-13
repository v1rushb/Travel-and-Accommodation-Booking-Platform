using TABP.Domain.Entities;

namespace TABP.Domain.Abstractions.Repositories;

public interface ICityRepository
{
    Task<Guid?> AddAsync(City newCity);
    Task<bool> ExistsAsync(Guid Id);
    Task<City> GetByIdAsync(Guid Id);
    Task DeleteAsync(Guid Id);
    Task UpdateAsync(Guid Id);
}