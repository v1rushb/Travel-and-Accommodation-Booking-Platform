using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Entities;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Application.Services;

public class RoomBookingService : IRoomBookingService
{
    private readonly IRoomBookingRepository _roomBookingRepository;
    private readonly ILogger<RoomBookingService> _logger;

    public RoomBookingService(
        IRoomBookingRepository roomBookingRepository,
        ILogger<RoomBookingService> logger)
    {
        _roomBookingRepository = roomBookingRepository;
        _logger = logger;
    }
    
    public async Task<Guid> AddAsync(RoomBookingDTO newBooking)
    {
        // do some validations later here.

        var bookingId = await _roomBookingRepository.AddAsync(newBooking);
        _logger.LogInformation("Booking with Id: {Id} has been added", bookingId);
        
        return bookingId;
    }

    public async Task DeleteAsync(Guid Id)
    {
        await ValidateId(Id);

        var booking = await _roomBookingRepository.GetByIdAsync(Id);
        _logger.LogInformation("Booking with Id: {Id} has been deleted", booking.Id);
    }

    public async Task<RoomBooking?> GetByIdAsync(Guid Id) =>
        await _roomBookingRepository.GetByIdAsync(Id);

    public async Task<IEnumerable<RoomBooking>> GetByRoomAsync(Guid roomId) =>
        await _roomBookingRepository.GetByRoomAsync(roomId);

    public async Task<IEnumerable<RoomBooking>> GetByUserAsync(Guid userId) =>
        await _roomBookingRepository.GetByUserAsync(userId);

    public async Task UpdateAsync(RoomBookingDTO updatedBooking)
    {
        updatedBooking.ModificationDate = DateTime.UtcNow;
        await _roomBookingRepository.UpdateAsync(updatedBooking);

        _logger.LogInformation("Updated RoomBooking with Id: {BookingId}", updatedBooking.Id);
    }
    public async Task<bool> ExistsAsync(Guid Id) =>
        await _roomBookingRepository.ExistsAsync(Id);

    private async Task ValidateId(Guid Id)
    {
        if (await ExistsAsync(Id))
        {
            throw new ArgumentException("Booking with Id already exists");
        }
    }
}