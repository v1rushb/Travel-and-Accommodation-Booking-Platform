using TABP.Domain.Models.City;
using TABP.Domain.Models.City.Response;
using TABP.Domain.Models.City.Search;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services;

public interface ICityService
{
    Task<CityDTO> GetByIdAsync(Guid Id);
    Task<Guid> AddAsync(CityDTO newCity);
    Task DeleteAsync(Guid Id);
    Task UpdateAsync(CityDTO updatedCity);
    Task<bool> ExistsAsync(Guid Id);
    Task<IEnumerable<CitySearchResponseDTO>> SearchAsync(CitySearchQuery query);
    Task<IEnumerable<CityAdminResponseDTO>> SearchForAdminAsync(CitySearchQuery query, PaginationDTO pagination); //add city added between time period?
}