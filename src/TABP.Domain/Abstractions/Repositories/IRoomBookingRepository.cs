using TABP.Domain.Entities;

namespace TABP.Domain.Abstractions.Repositories;

public interface IRoomBookingRepository
{
    Task<Guid> AddAsync(RoomBooking newBooking);

    bool RoomIsBookedBetween(Guid roomId, DateTime StartingDate, DateTime EndingDate);
}