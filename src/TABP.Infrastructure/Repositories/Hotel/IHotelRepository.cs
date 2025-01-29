using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel;
using TABP.Infrastructure.Extensions.Helpers;

namespace TABP.Infrastructure.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly IMapper _mapper;

    public HotelRepository(
        HotelBookingDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> AddAsync(HotelDTO newHotel)
    {
        var hotel = _mapper.Map<Hotel>(newHotel);
        var entityEntry =  _context.Hotels.Add(hotel);
        await _context.SaveChangesAsync();

        // _logger.LogInformation($"Created Hotel with Name: {hotel.Name}");
        return entityEntry.Entity.Id;
    }

    public async Task DeleteAsync(Guid Id)
    {
        _context.Hotels.Remove(new Hotel { Id = Id});

        await _context.SaveChangesAsync();

        // _logger.LogInformation($"Hotel with Id: {Id} has been deleted");
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _context.Hotels.AnyAsync(hotel => hotel.Id == Id);

    public async Task<HotelDTO> GetByIdAsync(Guid Id) =>
        _mapper.Map<HotelDTO>(
            await _context.Hotels
            .FirstOrDefaultAsync(hotel => hotel.Id == Id));
        
    public async Task UpdateAsync(HotelDTO updatedHotel)
    {
        var hotel = _mapper.Map<Hotel>(updatedHotel);
        _context.Hotels.Update(hotel);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetNextRoomNumberAsync(Guid hotelId) =>
        await _context.Hotels
            .Where(hotel => hotel.Id == hotelId)
            .Select(hotel => hotel.Rooms
                .Max(room => room.Number))
            .FirstOrDefaultAsync() + 1;

    public async Task<IEnumerable<HotelDTO>> SearchAsync(
        Expression<Func<Hotel, bool>> predicate,
        int pageNumber,
        int pageSize)
    {
        var hotels = await _context.Hotels
            .Where(predicate)
            .PaginateAsync(pageNumber, pageSize);

        return _mapper.Map<IEnumerable<HotelDTO>>(hotels);
    }
}