using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel;
using TABP.Infrastructure.Extensions.Helpers;

namespace TABP.Infrastructure.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<HotelRepository> _logger;

    public HotelRepository(
        HotelBookingDbContext context,
        IMapper mapper,
        ILogger<HotelRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Guid> AddAsync(HotelDTO newHotel)
    {
        var hotel = _mapper.Map<Hotel>(newHotel);
        var entityEntry =  _context.Hotels.Add(hotel);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Added Hotel: {Name} With Hotel Id: {Id}", newHotel.Name, entityEntry.Entity.Id);

        return entityEntry.Entity.Id;
    }

    public async Task DeleteAsync(Guid Id)
    {
        _context.Hotels.Remove(new Hotel { Id = Id});

        await _context.SaveChangesAsync();

        _logger.LogInformation("Deleted Hotel With Id: {Id}", Id);
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
        
        _logger.LogInformation("Updated Hotel: {Name} With Hotel Id: {Id}", updatedHotel.Name, hotel.Id);
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
        int pageSize,
        Func<IQueryable<Hotel>, IOrderedQueryable<Hotel>> orderBy = null)
    {
        var query = _context.Hotels
            .Include(h => h.City)
            .Where(predicate)
            .OrderByIf(orderBy != null, orderBy)
            .Paginate(
                pageNumber,
                pageSize
            );

        return _mapper
            .Map<IEnumerable<HotelDTO>>(query
                .ToList());
    }


    public async Task<string> GetHotelNameByIdAsync(Guid Id) =>
        (await _context.Hotels.FirstOrDefaultAsync(hotel => hotel.Id == Id))?.Name;
}