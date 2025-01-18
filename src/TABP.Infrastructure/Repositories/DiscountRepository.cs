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
        var discount = _mapper.Map<Discount>(newDiscount);
        discount.CreationDate = DateTime.UtcNow;
        discount.ModificationDate = DateTime.UtcNow;

        var entityEntry = _context.Discounts.Add(discount);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Created Discount with Id: {Id}, HotelId: {HotelId}", entityEntry.Entity.Id, entityEntry.Entity.HotelId);

        return entityEntry.Entity.Id;
    }

    public async Task DeleteAsync(Guid Id)
    {
        _context.Discounts.Remove(new Discount { Id = Id});

        await _context.SaveChangesAsync();
        _logger.LogInformation($"Discount with discount Id: {Id} has been deleted");
    }

    public async Task<Discount?> GetByIdAsync(Guid Id) =>
        await _context.Discounts.FirstOrDefaultAsync(discount => discount.Id == Id);

    public async Task UpdateAsync(DiscountDTO updatedDiscount)
    {
        var discount = _mapper.Map<Discount>(updatedDiscount);
        discount.ModificationDate = DateTime.UtcNow;

        _context.Discounts.Update(discount);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Updated Discount with Id: {Id}", updatedDiscount.Id);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _context.Discounts.AnyAsync(discount => discount.Id == Id);

    public async Task<IEnumerable<Discount>> GetByHotelAsync(Guid hotelId) =>
        await _context.Discounts.Where(discount => discount.HotelId == hotelId).ToListAsync();

    public async Task<DiscountDTO> GetHighestDiscountActiveForHotelAsync(Guid hotelId) => // there always should be one.
        _mapper.Map<DiscountDTO>(await _context.Discounts
            .Where(discount => discount.HotelId == hotelId && discount.EndingDate > DateTime.UtcNow)
            .OrderByDescending(discount => discount.AmountPercentage)
            .FirstAsync());
}