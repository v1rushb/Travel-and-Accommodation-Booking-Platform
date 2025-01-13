using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;

namespace TABP.Infrastructure.Repositories;

public class HotelRepository : IHotelRepository
{
    public Task<Guid> AddAsync(Hotel newHotel)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<Hotel?> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Guid Id)
    {
        throw new NotImplementedException();
    }
}