using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Exceptions;
using TABP.Domain.Models.CartItem;

namespace TABP.Application.Services;

public class CartItemService : ICartItemService
{
    private readonly ICartItemRepository _cartItemRepository;

    public CartItemService(
        ICartItemRepository cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }
    public async Task AddAsync(CartItemDTO newCartItem) =>
        await _cartItemRepository.AddAsync(newCartItem);

    public async Task DeleteAsync(Guid Id)
    {
        await ValidateId(Id);

        await _cartItemRepository.DeleteAsync(Id);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _cartItemRepository.ExistsAsync(Id);

    public async Task<CartItemDTO> GetByIdAsync(Guid Id)
    {
        await ValidateId(Id);

        return await _cartItemRepository
            .GetByIdAsync(Id);
    }

    private async Task ValidateId(Guid Id)
    {
        if(!await ExistsAsync(Id))
        {
            throw new EntityNotFoundException($"Id {Id} Does not exist.");
        }
    }
}