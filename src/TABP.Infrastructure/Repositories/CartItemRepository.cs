using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Models.CartItem;

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

        _logger.LogInformation(@"Added CartItem with Id: {CartItemId}
             for Room with Id: {RoomId} 
             With Time Interval: {CheckInDate} - {CheckOutDate}",
            
            entityEntry.Entity.Id,
            cartItem.RoomId,
            cartItem.CheckInDate,
            cartItem.CheckOutDate
        );

        return entityEntry.Entity.Id;
    }

    public async Task DeleteAsync(Guid Id)
    {
        _context.CartItems.Remove(new CartItem { Id = Id});

        await _context.SaveChangesAsync();
        
        _logger.LogInformation("Deleted CartItem with Id: {CartItemId}", Id);
    }

    public async Task<CartItemDTO> GetByIdAsync(Guid Id)
    {
        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(cartItem => cartItem.Id == Id);

        return _mapper.Map<CartItemDTO>(cartItem);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _context.CartItems.AnyAsync(cartItem => cartItem.Id == Id);

    public void Update(IEnumerable<CartItemDTO> cartItems)
    {
        var cartItemsToUpdate = _mapper.Map<IEnumerable<CartItem>>(cartItems);

        foreach (var cartItem in cartItemsToUpdate)
        {
            var existingEntity = _context.CartItems
                .Local
                .FirstOrDefault(entry => entry.Id == cartItem.Id);

            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            _context.CartItems.Update(cartItem);
        }

        _context.SaveChanges();
    }


    // public async Task<bool> ExistsBetweenUserAndRoomAsync(Guid userId, Guid roomId) =>
    //     await _context.CartItems.AnyAsync(cartItem => cartItem.UserId == userId && cartItem.RoomId == roomId);

    // public async Task<IEnumerable<CartItem>> GetByUserAsync(Guid userId) =>
    //     await _context.CartItems.Where(cartItem => cartItem.UserId == userId).ToListAsync();
}