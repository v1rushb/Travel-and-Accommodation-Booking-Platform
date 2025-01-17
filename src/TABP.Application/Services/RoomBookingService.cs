using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
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
    private readonly IValidator<RoomBookingDTO> _bookingValidator;
    private readonly ICurrentUserService _currentUserService;

    public RoomBookingService(
        IRoomBookingRepository roomBookingRepository,
        ILogger<RoomBookingService> logger,
        ICurrentUserService currentUserService)
    {
        _roomBookingRepository = roomBookingRepository;
        _logger = logger;
        _currentUserService = currentUserService;
    }
    
    public async Task<Guid> AddAsync(RoomBookingDTO newBooking)
    {
        var currentUserId = _currentUserService.GetUserId();
        newBooking.UserId = currentUserId;

        await _bookingValidator.ValidateAndThrowAsync(newBooking);


        newBooking.CreationDate = DateTime.UtcNow;
        newBooking.ModificationDate = DateTime.UtcNow;

        var bookingId = await _roomBookingRepository.AddAsync(newBooking);

        _logger.LogInformation("Booking with Id: {Id} has been added to User {UserId}", bookingId, currentUserId);
        
        return bookingId;
    }

    public async Task DeleteAsync(Guid Id)
    {
        var currentUserId = _currentUserService.GetUserId();

        var booking = await GetByIdAsync(Id);

        if (booking == null || booking.UserId != currentUserId) // make it more readable.
        {
            throw new KeyNotFoundException($"Booking with Id {Id} not found.");
        }

        await _roomBookingRepository.DeleteAsync(Id);
        _logger.LogInformation("Booking with Id: {Id} has been deleted from User {UserId}", Id, currentUserId);
    }

    public async Task<RoomBooking?> GetByIdAsync(Guid Id) =>
        await _roomBookingRepository.GetByIdAsync(Id);

    public async Task<IEnumerable<RoomBooking>> GetByRoomAsync(Guid roomId) =>
        await _roomBookingRepository.GetByRoomAsync(roomId);

    public async Task<IEnumerable<RoomBooking>> GetByUserAsync()
    {
        var currentUserId = _currentUserService.GetUserId();
        var bookings = await _roomBookingRepository.GetByUserAsync(currentUserId);

        _logger.LogInformation("User {UserId} requested bookings for himself", currentUserId);

        return bookings;
    }

    public async Task UpdateAsync(RoomBookingDTO updatedBooking)
    {
        await ValidateId(updatedBooking.Id);

        var currentUserId = _currentUserService.GetUserId();
        var booking = await GetByIdAsync(updatedBooking.Id);
        
        await ValidateOwnership(updatedBooking.Id, currentUserId);

        updatedBooking.ModificationDate = DateTime.UtcNow;
        await _roomBookingRepository.UpdateAsync(updatedBooking);

        _logger.LogInformation("Updated RoomBooking with Id: {BookingId}", updatedBooking.Id);
    }
    public async Task<bool> ExistsAsync(Guid Id) =>
        await _roomBookingRepository.ExistsAsync(Id);

    private async Task ValidateId(Guid Id)
    {
        if (!await ExistsAsync(Id))
        {
            throw new KeyNotFoundException("Booking does not exist.");
        }
    }

    private async Task ValidateOwnership(Guid bookingId, Guid currentUserId) 
    {
        var booking = await GetByIdAsync(bookingId);
        if(booking == null || booking.UserId != currentUserId)
        {
            throw new KeyNotFoundException($"Booking with Id {bookingId} not found.");
        }
    }

    public async Task<bool> RoomIsBookedBetween(Guid roomId, DateTime StartingDate, DateTime EndingDate) =>
        await _roomBookingRepository.RoomIsBookedBetween(roomId, StartingDate, EndingDate);
}