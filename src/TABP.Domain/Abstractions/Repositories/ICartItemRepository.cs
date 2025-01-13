using TABP.Domain.Entities;

namespace TABP.Domain.Abstractions.Repositories;

public interface ICartItemRepository
{
    Task<Guid> AddAsync(CartItem newCartItem);
    Task<bool> ExistsBetweenUserAndRoomAsync(Guid userId, Guid roomId);
}