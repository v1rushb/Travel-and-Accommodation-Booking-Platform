using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.Hotels;

namespace TABP.Domain.Abstractions.Repositories;

public interface IHotelRepository
{
    Task<Guid> AddAsync(HotelDTO newHotel);
    Task<bool> ExistsAsync(Guid Id);
    Task DeleteAsync(Guid Id);
    Task UpdateAsync(HotelDTO updatedHotel);
    Task<HotelDTO> GetByIdAsync(Guid Id);
    Task<int> GetNextRoomNumberAsync(Guid hotelId);
    Task<IEnumerable<HotelUserResponseDTO>> SearchAsync(Expression<Func<Hotel, bool>> predicate, int pageNumber, int pageSize);
    Task<IEnumerable<HotelAdminResponseDTO>> SearchAdminAsync(Expression<Func<Hotel, bool>> predicate, int pageNumber, int pageSize);
}