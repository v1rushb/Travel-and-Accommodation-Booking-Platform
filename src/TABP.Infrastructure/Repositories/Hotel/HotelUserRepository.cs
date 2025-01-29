using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Infrastructure.Extensions.Helpers;

namespace TABP.Infrastructure.Repositories;

public class HotelUserRepository : IHotelUserRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly IMapper _mapper;

    public HotelUserRepository(
        HotelBookingDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<HotelPageResponseDTO> GetHotelPageAsync(Guid hotelId)
    {
        var hotel = await _context.Hotels
            .Include(hotel => hotel.City)
            .Where(hotel => hotel.Id == hotelId)
            .FirstOrDefaultAsync();

        return _mapper.Map<HotelPageResponseDTO>(hotel);
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