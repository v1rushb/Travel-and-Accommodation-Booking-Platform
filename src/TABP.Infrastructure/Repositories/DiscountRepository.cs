using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Models.Discount;

namespace TABP.Infrastructure.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly ILogger<DiscountRepository> _logger;
    private readonly IMapper _mapper;

    public DiscountRepository(
        HotelBookingDbContext context,
        ILogger<DiscountRepository> logger,
        IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Guid> AddAsync(DiscountDTO newDiscount)
    {
        var entityEntry = _context.Discounts.Add(_mapper.Map<Discount>(newDiscount));

        await _context.SaveChangesAsync();

        return entityEntry.Entity.Id;
    }

    public async Task DeleteAsync(Guid Id)
    {
        _context.Discounts.Remove(new Discount { Id = Id});

        await _context.SaveChangesAsync();
        _logger.LogInformation($"Discount with discount Id: {Id} has been deleted");
    }

    public async Task<DiscountDTO?> GetByIdAsync(Guid Id) =>
        _mapper.Map<DiscountDTO>(await _context.Discounts.FirstOrDefaultAsync(discount => discount.Id == Id));

    public async Task UpdateAsync(DiscountDTO updatedDiscount)
    {
        
        _context.Discounts.Update(_mapper.Map<Discount>(updatedDiscount));
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _context.Discounts.AnyAsync(discount => discount.Id == Id);
}