using TABP.Domain.Enums;
using TABP.Domain.Models.City.Search;
using TABP.Domain.Models.City.Sort;
using TABP.Domain.Models.HotelVisit;
using TABP.Domain.Models.Pagination;
using TABP.Models.City;

namespace TABP.Domain.Abstractions.Services.City;

public interface ICityUserService
{
    Task<IEnumerable<CitySearchResponseDTO>> SearchAsync(
        CitySearchQuery query,
        PaginationDTO pagination,
        CitySortQuery sortQuery);

    Task<IEnumerable<CityVisitDTO>> GetTrendingCities(
        VisitTimeOptionQuery timeQuery,
        PaginationDTO pagination);
}