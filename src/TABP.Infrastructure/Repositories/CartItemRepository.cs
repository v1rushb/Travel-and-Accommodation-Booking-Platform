using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABD.Domain.Models.CartItem;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;

namespace TABP.Infrastructure.Repositories;

public class CartItemRepository : ICartItemRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CartItemRepository> _logger;

    public CartItemRepository(
        HotelBookingDbContext context,
        IMapper mapper,
        ILogger<CartItemRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Guid> AddAsync(CartItemDTO newCartItem)
    {
        var cartItem = _mapper.Map<CartItem>(newCartItem);
        cartItem.CreationDate = DateTime.UtcNow;

        var entityEntry = _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Added CartItem for User: {UserId}, Room: {RoomId}");
        
        return entityEntry.Entity.Id;
    }

    public async Task DeleteAsync(Guid Id)
    {
        _context.CartItems.Remove(new CartItem { Id = Id});

        await _context.SaveChangesAsync();
        _logger.LogInformation("Deleted CartItem with Id: {CartItemId}", Id);
    }

    public async Task<CartItem?> GetByIdAsync(Guid Id) =>
        await _context.CartItems.FirstOrDefaultAsync(cartItem => cartItem.Id == Id);

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _context.CartItems.AnyAsync(cartItem => cartItem.Id == Id);

    public async Task<bool> ExistsBetweenUserAndRoomAsync(Guid userId, Guid roomId) =>
        await _context.CartItems.AnyAsync(cartItem => cartItem.UserId == userId && cartItem.RoomId == roomId);

    public async Task<IEnumerable<CartItem>> GetByUserAsync(Guid userId) =>
        await _context.CartItems.Where(cartItem => cartItem.UserId == userId).ToListAsync();
}