using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
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
        visit.ModificationDate = DateTime.Now;

        var entityEntry = _context.HotelVisits.Add(visit);

        return entityEntry.Entity.Id;
    }

    public async Task DeleteAsync(Guid Id)
    {
        _context.HotelVisits.Remove(new HotelVisit { Id = Id});

        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _context.HotelVisits.AnyAsync(visit => visit.Id == Id);

    public async Task<IEnumerable<HotelVisit>> GetByHotelAsync(Guid hotelId) =>
        await _context.HotelVisits
            .Include(visit => visit.User)
            .Where(visit => visit.Id == hotelId)
            .ToListAsync();


    public async Task<HotelVisit?> GetByIdAsync(Guid Id) =>
        await _context.HotelVisits
            .Include(visit => visit.Hotel)
            .Include(visit => visit.User)
            .FirstOrDefaultAsync(visit => visit.Id == Id);

    public async Task<IEnumerable<HotelVisit>> GetByUserAsync(Guid userId) =>
        await _context.HotelVisits
            .Include(visit => visit.Hotel)
            .Where(visit => visit.UserId == userId)
            .ToListAsync();

    public async Task UpdateAsync(HotelVisitDTO updatedHotelVisit)
    {
        var visit = _mapper.Map<HotelVisit>(updatedHotelVisit);
        visit.ModificationDate = DateTime.UtcNow;

        var entityEntry = _context.HotelVisits.Update(visit);
        
        await _context.SaveChangesAsync();
    }
}