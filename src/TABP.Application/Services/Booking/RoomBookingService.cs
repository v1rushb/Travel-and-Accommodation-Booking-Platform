using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders.Generics;
using TABP.Domain.Utilities;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Abstractions.Services.Booking;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Exceptions;
using TABP.Domain.Models.Booking;
using TABP.Domain.Models.Cart;
using TABP.Domain.Models.HotelVisit;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Application.Services.Booking;

public class RoomBookingService : IRoomBookingService
{
    private readonly IRoomBookingRepository _roomBookingRepository;
    private readonly IValidator<RoomBookingDTO> _bookingValidator;
    private readonly IDiscountRepository _discountRepository;
    private readonly IRoomService _roomService;
    private readonly IRoomBookingEmailService _bookingEmailService;

    public RoomBookingService(
        IRoomBookingRepository roomBookingRepository,
        IDiscountRepository discountRepository,
        IRoomService roomService,
        IValidator<RoomBookingDTO> bookingValidator,
        IRoomBookingEmailService bookingEmailService)
    {
        _roomBookingRepository = roomBookingRepository;
        _discountRepository = discountRepository;
        _roomService = roomService;
        _bookingValidator = bookingValidator;
        _bookingEmailService = bookingEmailService;
    }
    
    public async Task AddAsync(CartDTO cart) // refactor method later.
    {
        var bookings = await CreateBookingsAsync(cart);

        foreach (var booking in bookings)
        {
            await ProcessBookingsAsync(booking);
        }

        await _roomBookingRepository.AddAsync(bookings);
    }

    private async Task<IEnumerable<RoomBookingDTO>> CreateBookingsAsync(CartDTO cart)
    {
        var bookings = new List<RoomBookingDTO>();

        foreach (var item in cart.Items)
        {
            var booking = new RoomBookingDTO
            {
                UserId = cart.UserId,
                RoomId = item.RoomId,
                CheckInDate = item.CheckInDate,
                CheckOutDate = item.CheckOutDate,
                Status = BookingStatus.Confirmed,
                CreationDate = DateTime.UtcNow,
                ModificationDate = DateTime.UtcNow,
                Notes = item.Notes,
            };

            await _bookingValidator.ValidateAndThrowAsync(booking);
            bookings.Add(booking);
        }
        return bookings;
    }

    private async Task ProcessBookingsAsync(RoomBookingDTO booking)
    {
        var room = await _roomService.GetByIdAsync(booking.RoomId);

        var discount = await _discountRepository
            .GetHighestDiscountActiveForHotelRoomTypeAsync(room.HotelId, room.Type);

        booking.TotalPrice = DiscountedPriceCalculator.GetFinalDiscountedPrice(
            booking.CheckInDate,
            booking.CheckOutDate,
            room.PricePerNight,
            discount.AmountPercentage);
        
        await _bookingEmailService
            .ScheduleSendingBookingEndedEmailJob(booking);

        await _bookingEmailService
            .ScheduleSendingNearEndingBookingEmailJob(booking);
    }

    public async Task<RoomBookingDTO> GetByIdAsync(Guid Id)
    {
        await ValidateId(Id);
        return await _roomBookingRepository
            .GetByIdAsync(Id);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _roomBookingRepository.ExistsAsync(Id);

    private async Task ValidateId(Guid Id)
    {
        if (!await ExistsAsync(Id))
        {
            throw new EntityNotFoundException("Booking does not exist.");
        }
    }

    public async Task<bool> RoomIsBookedBetween(Guid roomId, DateTime StartingDate, DateTime EndingDate) =>
        await _roomBookingRepository.RoomIsBookedBetween(roomId, StartingDate, EndingDate);

    public async Task<IEnumerable<HotelBookingDTO>> GetByHotelAsync()
    {
        var timeOption = TimeOptions.LastWeek;
        
        var timeOptionQuery = new VisitTimeOptionQuery
        {
            TimeOption = Enum.Parse(typeof(TimeOptions), timeOption.ToString())
                    .ToString()
        };

        var expression = TimeOptionExpressionBuilder<RoomBooking>.Build(timeOptionQuery);
        return await _roomBookingRepository
            .GetAllForHotelsAsync(expression);
    }
}