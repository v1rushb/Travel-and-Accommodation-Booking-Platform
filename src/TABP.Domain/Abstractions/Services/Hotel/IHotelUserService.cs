using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.Hotel.Search;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.HotelVisit;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services;

public interface IHotelUserService
{
    Task<IEnumerable<HotelUserResponseDTO>> SearchAsync(
        HotelSearchQuery query,
        PaginationDTO pagination);

    Task<HotelPageResponseDTO> GetHotelPageAsync(Guid hotelId);

    Task<IEnumerable<FeaturedHotelDTO>> GetWeeklyFeaturedHotelsAsync();
    
    Task<IEnumerable<HotelHistoryDTO>> GetHotelHistoryAsync(
        PaginationDTO pagination,
        VisitTimeOptionQuery query,
        Guid? userId = null);
}