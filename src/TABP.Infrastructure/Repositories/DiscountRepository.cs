using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;

namespace TABP.Infrastructure.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly ILogger<DiscountRepository> _logger;

    public DiscountRepository(
        HotelBookingDbContext context,
        ILogger<DiscountRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Guid> AddAsync(Discount newDiscount)
    {
        var entityEntry = _context.Discounts.Add(newDiscount);

        await _context.SaveChangesAsync();

        return entityEntry.Entity.Id;
    }

    public async Task DeleteAsync(Guid Id)
    {
        _context.Discounts.Remove(new Discount { Id = Id});

        await _context.SaveChangesAsync();
        _logger.LogInformation($"Discount with discount Id: {Id} has been deleted");
    }

    public async Task<IEnumerable<Discount>> GetAllAsync() =>
        await _context.Discounts.ToListAsync();

    public async Task<Discount?> GetByIdAsync(Guid Id) =>
        await _context.Discounts.FirstOrDefaultAsync(discount => discount.Id == Id);

    public async Task UpdateAsync(Discount updatedDiscount)
    {
        _context.Discounts.Update(updatedDiscount);
        await _context.SaveChangesAsync();
    }
}