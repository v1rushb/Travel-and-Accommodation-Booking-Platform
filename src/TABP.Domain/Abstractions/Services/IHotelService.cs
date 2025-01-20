using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.Hotels;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services;

public interface IHotelService
{
    Task<Guid> AddAsync(HotelDTO newHotel);
    Task UpdateAsync(HotelDTO updatedHotel);
    Task DeleteAsync(Guid Id);
    Task<HotelDTO> GetByIdAsync(Guid Id);
    Task<bool> ExistsAsync(Guid Id);
    Task<int> GetNextRoomNumberAsync(Guid hotelId);
    Task<IEnumerable<HotelUserResponseDTO>> SearchAsync(HotelSearchQuery query, PaginationDTO pagination);
    Task<IEnumerable<HotelAdminResponseDTO>> SearchAdminAsync(HotelSearchQuery query, PaginationDTO pagination);
}