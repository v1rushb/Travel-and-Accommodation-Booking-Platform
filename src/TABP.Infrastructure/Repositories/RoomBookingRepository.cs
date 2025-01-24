using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Models.CartItem;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Models.Booking.Search.Response;
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
    public async Task<Guid> AddAsync(RoomBookingDTO newBooking)
    {
        var booking = _mapper.Map<RoomBooking>(newBooking);
        booking.CreationDate = DateTime.UtcNow;
        booking.ModificationDate = DateTime.UtcNow;
        
        var entityEntry = _context.RoomBookings.Add(booking);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Created RoomBooking for RoomId: {RoomId}, UserId: {UserId}, BookingId: {BookingId}", newBooking.RoomId, newBooking.UserId, entityEntry.Entity.Id);
        // fix

        return entityEntry.Entity.Id;
    }

    public async Task AddAsync(List<RoomBookingDTO> bookings)
    {
        var bookingList = _mapper.Map<List<RoomBooking>>(bookings);
        await _context.RoomBookings.AddRangeAsync(bookingList);
        await _context.SaveChangesAsync();
    }

    public async Task<RoomBookingDTO> GetByIdAsync(Guid Id) =>
        _mapper.Map<RoomBookingDTO>(
            await _context.RoomBookings
            .FirstOrDefaultAsync(booking => booking.Id == Id)); // include user and room later if needed. (check ur need after implementing logic or final refactor.)

    public async Task<bool> RoomIsBookedBetween(Guid roomId, DateTime StartingDate, DateTime EndingDate) =>
        await _context.RoomBookings.AnyAsync(booking => booking.RoomId == roomId && booking.CheckInDate >= StartingDate && booking.CheckOutDate <= EndingDate);


    public async Task DeleteAsync(Guid Id)
    {
        _context.RoomBookings.Remove(new RoomBooking { Id = Id});
        await _context.SaveChangesAsync();
        _logger.LogInformation("Deleted RoomBooking with Id: {BookingId}", Id);
    }

    public async Task<IEnumerable<RoomBooking>> GetByRoomAsync(Guid roomId) =>
        await _context.RoomBookings.Where(booking => booking.RoomId == roomId).ToListAsync();

    public async Task<IEnumerable<RoomBooking>> GetByUserAsync(Guid userId) =>
        await _context.RoomBookings.Where(booking => booking.UserId == userId).ToListAsync();

    public async Task UpdateAsync(RoomBookingDTO updatedBooking)
    {
        var booking = _mapper.Map<RoomBooking>(updatedBooking);
        booking.ModificationDate = DateTime.UtcNow;

        _context.RoomBookings.Update(booking);
        _logger.LogInformation("Updated RoomBooking with BookingId: {BookingId}", updatedBooking.Id);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _context.RoomBookings.AnyAsync(booking => booking.Id == Id);

    public async Task<IEnumerable<BookingUserResponseDTO>> SearchUserBookingsAsync(
        Expression<Func<RoomBooking, bool>> predicate,
        int pageNumber,
        int pageSize)
    {
        var bookings = await _context.RoomBookings
            .Where(predicate)
            .PaginateAsync(
                pageNumber,
                pageSize);

        return _mapper.Map<IEnumerable<BookingUserResponseDTO>>(bookings);
    }

    public async Task<IEnumerable<BookingAdminResponseDTO>> SearchAdminAsync(
        Expression<Func<RoomBooking, bool>> predicate,
        int pageNumber,
        int pageSize)
    {
        var bookings = await _context.RoomBookings
            .Where(predicate)
            .PaginateAsync(
                pageNumber,
                pageSize);

        return _mapper.Map<IEnumerable<BookingAdminResponseDTO>>(bookings);
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