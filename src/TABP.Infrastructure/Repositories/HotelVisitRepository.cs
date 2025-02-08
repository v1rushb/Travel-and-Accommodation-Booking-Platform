using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.HotelVisit;

namespace TABP.Infrastructure.Repositories;

public class HotelVisitRepository : IHotelVisitRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<HotelVisitRepository> _logger;
    
    public HotelVisitRepository(
        HotelBookingDbContext context,
        IMapper mapper,
        ILogger<HotelVisitRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Guid> AddAsync(HotelVisitDTO newHotelVisit)
    {
        var visit = _mapper.Map<HotelVisit>(newHotelVisit);
        visit.CreationDate = DateTime.Now;

        var entityEntry = _context.HotelVisits.Add(visit);

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            @"Created Hotel Visit With Id: {VisitId}
            Hotel with Id: {HotelId} 
            Made by User with Id {UserId}",
            
            entityEntry.Entity.Id,
            entityEntry.Entity.HotelId,
            entityEntry.Entity.UserId);

        return entityEntry.Entity.Id;
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _context.HotelVisits.AnyAsync(visit => visit.Id == Id);

    public async Task<IEnumerable<VisitedHotelDTO>> GetTop5VisitedHotels(
        Expression<Func<HotelVisit, bool>> predicate)
    {
        var hotels = await _context.HotelVisits
            .Where(predicate)
            .GroupBy(visit => visit.HotelId)
            .Select(group => new VisitedHotelDTO
            {
                Id = group.Key,
                Name = group.First().Hotel.Name,
                Visits = group.Count(),
                StarRating = group.First().Hotel.StarRating
            })
            .OrderByDescending(hotel => hotel.Visits)
            .Take(5)
            .ToListAsync();

        return hotels;
    }
}