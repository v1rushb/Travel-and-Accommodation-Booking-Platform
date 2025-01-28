using TABP.Domain.Abstractions.Services;

namespace TABP.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly HotelBookingDbContext _context;

    public UnitOfWork(HotelBookingDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}