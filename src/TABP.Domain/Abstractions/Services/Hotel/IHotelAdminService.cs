using TABP.Domain.Models.Hotel.Search;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services.Hotel;

public interface IHotelAdminService
{
    Task<IEnumerable<HotelAdminResponseDTO>> SearchAsync(HotelSearchQuery query, PaginationDTO pagination);
}