using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Models.RoomBooking;
using TABP.Infrastructure.Extensions.Helpers;
using TABP.Domain.Models.Booking;

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

    public async Task AddAsync(IEnumerable<RoomBookingDTO> bookings)
    {
        var bookingList = _mapper.Map<List<RoomBooking>>(bookings);
        await _context.RoomBookings.AddRangeAsync(bookingList);
        await _context.SaveChangesAsync();

        foreach(var booking in bookings)
        {
            _logger.LogInformation(
                @"Added RoomBooking with BookingId: {BookingId}
                For Room with Id: {RoomId}
                By User with Id: {UserId}",

                booking.Id,
                booking.RoomId,
                booking.UserId);
        }
    }

    public async Task<RoomBookingDTO> GetByIdAsync(Guid Id) =>
        _mapper.Map<RoomBookingDTO>(
            await _context.RoomBookings
            .FirstOrDefaultAsync(booking => booking.Id == Id)); // include user and room later if needed. (check ur need after implementing logic or final refactor.)

    public async Task<bool> RoomIsBookedBetween(Guid roomId, DateTime StartingDate, DateTime EndingDate) =>
        await _context.RoomBookings.AnyAsync(booking => booking.RoomId == roomId && booking.CheckInDate >= StartingDate && booking.CheckOutDate <= EndingDate);

    public async Task<IEnumerable<RoomBooking>> GetByRoomAsync(Guid roomId) =>
        await _context.RoomBookings.Where(booking => booking.RoomId == roomId).ToListAsync();

    public async Task<IEnumerable<RoomBooking>> GetByUserAsync(Guid userId) =>
        await _context.RoomBookings.Where(booking => booking.UserId == userId).ToListAsync();

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _context.RoomBookings.AnyAsync(booking => booking.Id == Id);

    public async Task<IEnumerable<RoomBookingDTO>> SearchAsync(
        Expression<Func<RoomBooking, bool>> predicate,
        int pageNumber,
        int pageSize,
        Func<IQueryable<RoomBooking>, IOrderedQueryable<RoomBooking>> orderByDelegate = null)
    {
        var bookings = await _context.RoomBookings
            .Where(predicate)
            .OrderByIf(orderByDelegate != null, orderByDelegate)
            .PaginateAsync(
                pageNumber,
                pageSize
            );

        return _mapper
            .Map<IEnumerable<RoomBookingDTO>>(bookings);
    }

    public async Task<IEnumerable<HotelBookingDTO>> GetAllForHotelsAsync(Expression<Func<RoomBooking, bool>> predicate)
    {
        var bookings = await _context.RoomBookings
            .Where(predicate)
            .Select(booking => new HotelBookingDTO
            {
                BookingId = booking.Id,
                HotelId = booking.Room.HotelId
            }).ToListAsync();

        return bookings;
    }

}