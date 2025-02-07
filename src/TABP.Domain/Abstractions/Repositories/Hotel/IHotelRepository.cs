using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel;

namespace TABP.Domain.Abstractions.Repositories;

public interface IHotelRepository
{
    Task<Guid> AddAsync(HotelDTO newHotel);
    Task<bool> ExistsAsync(Guid Id);
    Task DeleteAsync(Guid Id);
    Task UpdateAsync(HotelDTO updatedHotel);
    Task<HotelDTO> GetByIdAsync(Guid Id);
    Task<int> GetNextRoomNumberAsync(Guid hotelId);
    Task<IEnumerable<HotelDTO>> SearchAsync(
        Expression<Func<Hotel, bool>> predicate,
        int pageNumber,
        int pageSize,
        Func<IQueryable<Hotel>,IOrderedQueryable<Hotel>> orderBy 
            = null);
    Task<string> GetHotelNameByIdAsync(Guid Id);
    void Update(HotelDTO Hotel);
}