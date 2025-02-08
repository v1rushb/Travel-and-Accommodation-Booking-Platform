using TABP.Domain.Models.City.Search;
using TABP.Domain.Models.City.Sort;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services.City;

public interface ICityUserService
{
    Task<IEnumerable<CitySearchResponseDTO>> SearchAsync(
        CitySearchQuery query,
        PaginationDTO pagination,
        CitySortQuery sortQuery);
}