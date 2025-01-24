using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Models.Discount;
using TABP.Domain.Models.Discount.Search.Response;
using TABP.Infrastructure.Extensions.Helpers;

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

    public async Task<DiscountDTO> GetByIdAsync(Guid Id) =>
        _mapper.Map<DiscountDTO>(await _context.Discounts
            .FirstOrDefaultAsync(discount => discount.Id == Id));

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
    public async Task<IEnumerable<DiscountForAdminResponseDTO>> SearchForAdminAsync(
        Expression<Func<Discount, bool>> predicate,
        int pageNumber,
        int pageSize)
    {
        var discounts = await _context.Discounts
            .Where(predicate)
            .PaginateAsync(pageNumber, pageSize);

        return _mapper.Map<IEnumerable<DiscountForAdminResponseDTO>>(discounts);
    }

    public async Task<DiscountDTO> GetHighestDiscountActiveForHotelRoomTypeAsync(Guid hotelId, RoomType type)
    {
         var maxDiscount = await _context.Discounts
        .Where(discount => discount.roomType == type && discount.HotelId == hotelId)
        .OrderByDescending(discount => discount.AmountPercentage)
        .FirstOrDefaultAsync();

        return maxDiscount != null
            ? _mapper.Map<DiscountDTO>(maxDiscount)
            : new DiscountDTO { AmountPercentage = 0 };
    }

    public async Task<IEnumerable<DiscountDTO>> GetActiveDiscountsForHotelAsync(Guid hotelId)
    {
        var discounts = await _context.Discounts
            .Where(discount => discount.HotelId == hotelId && discount.EndingDate > DateTime.UtcNow)
            .ToListAsync();
        
        return _mapper.Map<IEnumerable<DiscountDTO>>(discounts);
    }
}