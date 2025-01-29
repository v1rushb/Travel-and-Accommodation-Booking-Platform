using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Exceptions;
using TABP.Domain.Models.CartItem;

namespace TABP.Application.Services;

public class CartItemService : ICartItemService
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly ILogger<CartItemService> _logger;

    public CartItemService(
        ICartItemRepository cartItemRepository,
        ILogger<CartItemService> logger)
    {
        _cartItemRepository = cartItemRepository;
        _logger = logger;
    }
    public async Task AddAsync(CartItemDTO newCartItem)
    {
        // some validations here.

        var cartItemId = await _cartItemRepository.AddAsync(newCartItem);

        // _logger.LogInformation("Added CartItem for User: {UserId}, Room: {RoomId}", newCartItem.UserId, newCartItem.RoomId);
    }

    public async Task DeleteAsync(Guid Id)
    {
        await ValidateId(Id);

        await _cartItemRepository.DeleteAsync(Id);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _cartItemRepository.ExistsAsync(Id);

    public async Task<CartItemDTO> GetByIdAsync(Guid Id) =>
        await _cartItemRepository.GetByIdAsync(Id);

    private async Task ValidateId(Guid Id)
    {
        if(!await ExistsAsync(Id))
        {
            throw new NotFoundException($"Id {Id} Does not exist.");
        }
    }
}