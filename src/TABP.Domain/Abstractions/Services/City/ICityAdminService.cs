using TABP.Domain.Models.City.Response;
using TABP.Domain.Models.City.Search;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services.City;

public interface ICityAdminService
{
    Task<IEnumerable<CityAdminResponseDTO>> SearchAsync(CitySearchQuery query, PaginationDTO pagination); //add city added between time period?
}