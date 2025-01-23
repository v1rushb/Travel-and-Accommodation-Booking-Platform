using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.Hotels;
using TABP.Domain.Models.HotelVisit;

namespace TABP.Infrastructure.Repositories;

public class HotelVisitRepository : IHotelVisitRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly IMapper _mapper;
    
    public HotelVisitRepository(
        HotelBookingDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> AddAsync(HotelVisitDTO newHotelVisit)
    {
        var visit = _mapper.Map<HotelVisit>(newHotelVisit);
        visit.CreationDate = DateTime.Now;

        var entityEntry = _context.HotelVisits.Add(visit);

        await _context.SaveChangesAsync();

        return entityEntry.Entity.Id;
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _context.HotelVisits.AnyAsync(visit => visit.Id == Id);

    public async Task<IEnumerable<HotelVisit>> GetByHotelAsync(Guid hotelId, DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.HotelVisits
            .Where(visit => visit.HotelId == hotelId);

        query = ApplyDateFilter(query, startDate, endDate);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<HotelVisit>> GetByUserAsync(Guid userId, DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.HotelVisits
            .Where(visit => visit.UserId == userId);

        query = ApplyDateFilter(query, startDate, endDate);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<HotelVisit>> GetByUserAndHotelAsync(
        Guid userId,
        Guid hotelId,
        DateTime? startDate = null,
        DateTime? endDate = null)
    {
        var query = _context.HotelVisits
            .Where(visit => visit.UserId == userId && visit.HotelId == hotelId);

        query = ApplyDateFilter(query, startDate, endDate);

        return await query.ToListAsync();
    }

    private IQueryable<HotelVisit> ApplyDateFilter(IQueryable<HotelVisit> visits, DateTime? startDate = null, DateTime? endDate = null)
    {
        if(startDate.HasValue)
        {
            visits = visits
                .Where(visit => visit.CreationDate >= startDate.Value);
        }

        if(endDate.HasValue)
        {
            visits = visits
                .Where(visit => visit.CreationDate <= endDate.Value);
        }

        return visits;
    }

    public async Task<IEnumerable<VisitedHotelDTO>> GetTop5VisitedHotels(
        Expression<Func<HotelVisit, bool>> predicate)
    {
        var hotels = await _context.HotelVisits
            // .Where(predicate)
            .GroupBy(visit => visit.HotelId)
            .Select(group => new VisitedHotelDTO
            {
                Id = group.Key,
                Name = group.First().Hotel.Name,
                Visits = group.Count()
            })
            .OrderByDescending(hotel => hotel.Visits)
            .Take(5)
            .ToListAsync();

        return hotels;
    }
}