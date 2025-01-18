using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Entities;
using TABP.Domain.Models.Discount;
using TABP.Domain.Models.Room;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Application.Services;

public class RoomBookingService : IRoomBookingService
{
    private readonly IRoomBookingRepository _roomBookingRepository;
    private readonly ILogger<RoomBookingService> _logger;
    private readonly IValidator<RoomBookingDTO> _bookingValidator;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDiscountRepository _discountRepository;
    private readonly IRoomService _roomService;

    public RoomBookingService(
        IRoomBookingRepository roomBookingRepository,
        ILogger<RoomBookingService> logger,
        ICurrentUserService currentUserService,
        IDiscountRepository discountRepository,
        IRoomService roomService)
    {
        _roomBookingRepository = roomBookingRepository;
        _logger = logger;
        _currentUserService = currentUserService;
        _discountRepository = discountRepository;
        _roomService = roomService;
    }
    
    public async Task<Guid> AddAsync(RoomBookingDTO newBooking)
    {
        var currentUserId = _currentUserService.GetUserId();
        newBooking.UserId = currentUserId;


        await _bookingValidator.ValidateAndThrowAsync(newBooking);

        newBooking.CreationDate = DateTime.UtcNow;
        newBooking.ModificationDate = DateTime.UtcNow;

        var room = await _roomService.GetByIdAsync(newBooking.RoomId);
        var discount = await _discountRepository.GetHighestDiscountActiveForHotelAsync(room.HotelId);
        
        SetFinalTotalPrice(newBooking, room, discount);

        var bookingId = await _roomBookingRepository.AddAsync(newBooking);
    
        _logger.LogInformation("Booking with Id: {Id} has been added to User {UserId}", bookingId, currentUserId);
        
        return bookingId;
    }

    public async Task DeleteAsync(Guid Id)
    {
        var currentUserId = _currentUserService.GetUserId();

        await ValidateOwnership(Id, currentUserId);

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
    
    private void SetFinalTotalPrice(RoomBookingDTO booking, RoomDTO room, DiscountDTO discount)
    {
        var discountPercentage = discount?.AmountPercentage ?? 0; // temp sol, make sure to include 0% by default to db.
        var originalPrice = ((booking.CheckOutDate - booking.CheckInDate).Days + 1) * room.PricePerNight;
        var discountedPrice = ApplyDiscount(originalPrice, discountPercentage);
        booking.TotalPrice = discountedPrice;
    }

    private static decimal ApplyDiscount(int originalPrice, decimal discountPercentage) =>
        originalPrice - (originalPrice * (discountPercentage / 100));
}