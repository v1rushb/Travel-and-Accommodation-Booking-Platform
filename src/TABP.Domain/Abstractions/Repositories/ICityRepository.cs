using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.City;
using TABP.Domain.Models.City.Response;
using TABP.Domain.Models.City.Search;

namespace TABP.Domain.Abstractions.Repositories;

public interface ICityRepository
{
    Task<Guid> AddAsync(CityDTO newCity);
    Task<bool> ExistsAsync(Guid Id);
    Task<CityDTO?> GetByIdAsync(Guid Id);
    Task DeleteAsync(Guid Id);
    Task UpdateAsync(CityDTO updatedCity);
    Task<IEnumerable<CitySearchResponseDTO>> SearchAsync(Expression<Func<City, bool>> predicate, int pageNumber, int pageSize);
    Task<IEnumerable<CityAdminResponseDTO>> SearchForAdminAsync(Expression<Func<City, bool>> predicate, int pageNumber, int pageSize); 
    Task<bool> ExistsByNameAndCountryAsync(string name, string country);
}