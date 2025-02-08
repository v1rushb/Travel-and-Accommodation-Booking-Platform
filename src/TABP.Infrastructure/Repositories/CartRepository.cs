using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Models.Cart;
using TABP.Domain.Models.Cart.Search.Response;
using TABP.Domain.Models.CartItem;
using TABP.Infrastructure.Extensions.Helpers;

namespace TABP.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CartRepository> _logger;

    public CartRepository(
        HotelBookingDbContext context,
        IMapper mapper,
        ILogger<CartRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Guid> CreateAsync(CartDTO newCartItem)
    {
        var cart = _mapper.Map<Cart>(newCartItem); // add mapping here. ----------------------------------------------------
        
        var entity = await _context.Carts.AddAsync(cart);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Created Cart with Id: {CartId} for User: {UserId}",
            entity.Entity.Id,
            cart.UserId);

        return entity.Entity.Id;
    }

    public async Task DeleteAsync(Guid Id)
    {
        _context.Carts.Remove(new Cart { Id = Id });
        await _context.SaveChangesAsync();

        _logger.LogInformation("Deleted Cart with Id: {CartId}", 
            Id);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _context.Carts.AnyAsync(cart => cart.Id == Id);

    public async Task<CartDTO> GetByIdAsync(Guid Id)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == Id);

        return _mapper.Map<CartDTO>(cart);
    }

    public async Task<CartDTO> GetLastPendingCartAsync(Guid userId)
    {
        var cart = await _context.Carts
            .Include(cart => cart.Items)
            .Where(cart => cart.UserId == userId && cart.Status == BookingStatus.Pending)
            .OrderByDescending(cart => cart.CreationDate)
            .FirstOrDefaultAsync();

        return _mapper.Map<CartDTO>(cart);
    }

    public async Task AddItemAsync(CartItemDTO newItem)
    {
        var cartItem = _mapper.Map<CartItem>(newItem);
        cartItem.CreationDate = DateTime.UtcNow;

        _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Added CartItem with Id: {CartItemId} To Cart with Id: {CartId}",
            cartItem.Id,
            cartItem.CartId);
    }

    public async Task DeleteItemAsync(Guid cartItemId)
    {
        _context.CartItems.Remove(new CartItem { Id = cartItemId });
        await _context.SaveChangesAsync();
    }

    public async Task UpdateItemAsync(CartItemDTO cartItem)
    {
        var cartItemToUpdate = _mapper.Map<CartItem>(cartItem);
        _context.CartItems.Update(cartItemToUpdate);

        await _context.SaveChangesAsync();

        _logger.LogInformation("Updated CartItem with Id: {CartItemId}", cartItem.Id);
    }

    public async Task<IEnumerable<CartItemDTO>> GetAllCartItemsAsync(
        Guid cartId,
        int pageNumber,
        int pageSize)
    {
        var cartItems = await _context.Carts
            .Where(cart => cart.Id == cartId)
            .SelectMany(cart => cart.Items)
            .PaginateAsync(pageNumber, pageSize);


        return _mapper.Map<IEnumerable<CartItemDTO>>(cartItems);
    }

    public async Task UpdateAsync(CartDTO updatedCart)
    {
        var cart = _mapper.Map<Cart>(updatedCart);

        _context.Carts.Update(cart);

        await _context.SaveChangesAsync();

        _logger.LogInformation("Updated Cart with Id: {CartId}",
            updatedCart.Id);
    }

    public async Task<bool> RoomIsBookedForSameUserBetween(
        Guid roomId,
        Guid userId,
        DateTime startingDate,
        DateTime endingDate)
    {
        return await _context.Carts
            .Where(cart => cart.UserId == userId && cart.Status == BookingStatus.Pending)
            .SelectMany(cart => cart.Items)
            .AnyAsync(cartItem =>
                cartItem.RoomId == roomId &&
                cartItem.CheckInDate < endingDate &&
                cartItem.CheckOutDate > startingDate);
    }

    public async Task<bool> RoomIsBookedByAnyUserBetween(
        Guid roomId,
        DateTime startingDate,
        DateTime endingDate)
    {
        return await _context.Carts
            .Where(cart => cart.Status == BookingStatus.Confirmed)
            .SelectMany(cart => cart.Items)
            .AnyAsync(cartItem =>
                cartItem.RoomId == roomId &&
                cartItem.CheckInDate < endingDate &&
                cartItem.CheckOutDate > startingDate);
    }

    public async Task<IEnumerable<CartAdminResponseDTO>> SearchAdminAsync(
        Expression<Func<Cart, bool>> predicate,
        int pageNumber,
        int pageSize,
        Func<IQueryable<Cart>, IOrderedQueryable<Cart>> orderByDelegate = null)
    {
        var carts = await _context.Carts
            .Include(cart => cart.Items)
            .Where(predicate)
            .OrderByIf(orderByDelegate != null, orderByDelegate)
            .PaginateAsync(
                pageNumber,
                pageSize
            );

        return _mapper
            .Map<IEnumerable<CartAdminResponseDTO>>(carts);
    }

    public async Task<CartUserResponseDTO> GetCartDetailsByUserIdAsync(Guid userId) =>
        _mapper.Map<CartUserResponseDTO>(
                await GetLastPendingCartAsync(userId));
}