using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.Hotels;
using TABP.Infrastructure.Extensions.Helpers;

namespace TABP.Infrastructure.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly ILogger<HotelRepository> _logger;
    private readonly IMapper _mapper;

    public HotelRepository(
        HotelBookingDbContext context,
        ILogger<HotelRepository> logger,
        IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Guid> AddAsync(HotelDTO newHotel)
    {
        var hotel = _mapper.Map<Hotel>(newHotel);
        var entityEntry =  _context.Hotels.Add(hotel);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Created Hotel with Name: {hotel.Name}");
        return entityEntry.Entity.Id;
    }

    public async Task DeleteAsync(Guid Id)
    {
        _context.Hotels.Remove(new Hotel { Id = Id});

        await _context.SaveChangesAsync();

        _logger.LogInformation($"Hotel with Id: {Id} has been deleted");
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _context.Hotels.AnyAsync(hotel => hotel.Id == Id);

    public async Task<Hotel?> GetByIdAsync(Guid Id) =>
        await _context.Hotels.FirstOrDefaultAsync(hotel => hotel.Id == Id);
        
    public async Task UpdateAsync(HotelDTO updatedHotel)
    {
        var hotel = _mapper.Map<Hotel>(updatedHotel);
        _context.Hotels.Update(hotel);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<HotelUserResponseDTO>> SearchAsync(
        Expression<Func<Hotel, bool>> predicate,
        int pageNumber,
        int pageSize)
        {
            var hotels =  await _context.Hotels
                .Where(predicate)
                .PaginateAsync(pageNumber, pageSize);

            return _mapper.Map<IEnumerable<HotelUserResponseDTO>>(hotels);
        }
}