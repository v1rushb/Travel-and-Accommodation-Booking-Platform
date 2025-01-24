using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel;
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

    public async Task<IEnumerable<HotelAdminResponseDTO>> SearchAdminAsync(
        Expression<Func<Hotel, bool>> predicate,
        int pageNumber,
        int pageSize)
    {
        var hotels =  await _context.Hotels
            .Where(predicate)
            .PaginateAsync(
            pageNumber
            ,pageSize);
        
        return _mapper.Map<IEnumerable<HotelAdminResponseDTO>>(hotels);
    }

    public async Task<int> GetNextRoomNumberAsync(Guid hotelId) =>
        await _context.Hotels
            .Where(hotel => hotel.Id == hotelId)
            .Select(hotel => hotel.Rooms
                .Max(room => room.Number))
            .FirstOrDefaultAsync() + 1;

    public async Task<HotelPageResponseDTO> GetHotelPageAsync(Guid hotelId)
    {
        var hotel = await _context.Hotels
            .Include(hotel => hotel.City)
            .Where(hotel => hotel.Id == hotelId)
            .FirstOrDefaultAsync();

        var x= _mapper.Map<HotelPageResponseDTO>(hotel);
        return x;
    }

    public async Task<IEnumerable<VisitedHotelDTO>> GetWeeklyFeaturedHotelsAsync(
        Expression<Func<Hotel, bool>> predicate)
    {
        var hotels = await _context.Hotels
            .Where(predicate)
            .GroupBy(hotel => hotel.Id)
            .Select(group => new VisitedHotelDTO
            {
                Id = group.Key,
                Visits = group.Count(),
                StarRating = group.Max(hotel =>
                     hotel.StarRating),
                Name = group.First().Name
            })
            .OrderByDescending(hotel => hotel.Visits)
            .Take(5)
            .ToListAsync();

        return hotels;
    }
    public async Task<IEnumerable<HotelHistoryDTO>> GetHotelHistoryAsync(
        Expression<Func<HotelVisit, bool>> predicate,
        Guid userId, 
        int pageNumber, 
        int pageSize)
    {
        var query = await _context.Hotels
        .Where(hotel => hotel.HotelVisits
            .Any(visit => visit.UserId == userId))
        .Select(hotel => new
        {
            Hotel = hotel,
            Visits = hotel.HotelVisits
                .Where(visit => visit.UserId == userId)
        })
        .ToListAsync();

        var filtered = query
            .Select(hotel => new HotelHistoryDTO{
                HotelId = hotel.Hotel.Id,
                Name = hotel.Hotel.Name,
                BriefDescription = hotel.Hotel.BriefDescription,
                StarRating = hotel.Hotel.StarRating,
                LastVisitDate = hotel.Visits
                    .Where(predicate.Compile())
                    .OrderByDescending(visit => visit.CreationDate)
                    .Select(visit => visit.CreationDate)
                    .FirstOrDefault()
            })
            .Where(hotel => hotel.LastVisitDate != default)
            .OrderByDescending(hotel => hotel.LastVisitDate)
            .PaginateAsync(
                pageNumber,
                pageSize);

        return filtered;
    }
}