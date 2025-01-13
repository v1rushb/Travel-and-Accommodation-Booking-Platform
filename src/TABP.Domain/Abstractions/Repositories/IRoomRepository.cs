using TABP.Domain.Entities;

namespace TABP.Domain.Abstractions.Repositories;

public interface IRoomRepository
{
    Task<Guid> AddAsync(Room newRoom);
    Task<bool> ExistsAsync(Guid Id);
    Task DeleteAsync(Guid Id);
    Task UpdateAsync(Guid Id);
    Task<Room> GetByIdAsync(Guid Id); 
}