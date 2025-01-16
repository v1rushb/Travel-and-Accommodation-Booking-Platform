using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;

namespace TABP.Infrastructure.Repositories;

public class RoomBookingRepository : IRoomBookingRepository
{

    private readonly HotelBookingDbContext _context;
    private readonly ILogger<RoomBookingRepository> _logger;
    private readonly IMapper _mapper;

    public RoomBookingRepository(
        HotelBookingDbContext context,
        ILogger<RoomBookingRepository> logger,
        IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<Guid> AddAsync(RoomBookingDTO newBooking)
    {
        var booking = _mapper.Map<RoomBooking>(newBooking);
        var entityEntry = _context.RoomBookings.Add(booking);
        await _context.SaveChangesAsync();

        return entityEntry.Entity.Id;
    }

    public async Task<RoomBooking?> GetByIdAsync(Guid Id) =>
        await _context.RoomBookings.FirstOrDefaultAsync(booking => booking.Id == Id);

    public async Task<bool> RoomIsBookedBetween(Guid roomId, DateTime StartingDate, DateTime EndingDate) =>
        await _context.RoomBookings.AnyAsync(booking => booking.RoomId == roomId && booking.CheckInDate >= StartingDate && booking.CheckOutDate <= EndingDate);
}