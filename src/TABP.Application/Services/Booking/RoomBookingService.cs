using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders.Generics;
using TABP.Application.Utilities;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Abstractions.Services.Booking;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Models.Booking;
using TABP.Domain.Models.Cart;
using TABP.Domain.Models.Email;
using TABP.Domain.Models.HotelVisit;
using TABP.Domain.Models.RoomBooking;
using TABP.Domain.Models.User;

namespace TABP.Application.Services.Booking;

public class RoomBookingService : IRoomBookingService
{
    private readonly IRoomBookingRepository _roomBookingRepository;
    private readonly ILogger<RoomBookingService> _logger;
    private readonly IValidator<RoomBookingDTO> _bookingValidator;
    private readonly IDiscountRepository _discountRepository;
    private readonly IRoomService _roomService;
    private readonly ICacheEventService _cacheEventService;
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepository;

    public RoomBookingService(
        IRoomBookingRepository roomBookingRepository,
        ILogger<RoomBookingService> logger,
        IDiscountRepository discountRepository,
        IRoomService roomService,
        IValidator<RoomBookingDTO> bookingValidator,
        ICacheEventService cacheEventService,
        IEmailService emailService,
        IUserRepository userRepository)
    {
        _roomBookingRepository = roomBookingRepository;
        _logger = logger;
        _discountRepository = discountRepository;
        _roomService = roomService;
        _bookingValidator = bookingValidator;
        _cacheEventService = cacheEventService;
        _emailService = emailService;
        _userRepository = userRepository;
    }
    
    public async Task AddAsync(CartDTO cart) // refactor method later.
    {
        var items = cart.Items;
        var bookings = new List<RoomBookingDTO>();
        foreach(var item in items)
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

        foreach(var booking in bookings)
        {
            var room = await _roomService.GetByIdAsync(booking.RoomId);
            var discount = await _discountRepository.GetHighestDiscountActiveForHotelRoomTypeAsync(room.HotelId, room.Type);

            booking.TotalPrice = DiscountedPriceCalculator.GetFinalDiscountedPrice(
                booking.CheckInDate,
                booking.CheckOutDate,
                room.PricePerNight,
                discount.AmountPercentage);

            // _logger.LogInformation("Booking with Id: {Id} has been added to User {UserId}, with Discount {Discount}%", booking.Id, cart.UserId, discount.AmountPercentage);
            
            await SchduleSendingBookingEndedEmailJob(booking);
        }
        await _roomBookingRepository.AddAsync(bookings); // just adds range of bookings.
    }

    private async Task SchduleSendingBookingEndedEmailJob(RoomBookingDTO booking)
    {
        var user = await GetCorrespondingUser(booking.UserId);
        var timeToSendEmail = booking.CheckOutDate - DateTime.UtcNow;
        await _cacheEventService.ScheduleExpirationAsync(
            new Guid().ToString(),
            timeToSendEmail,
            async () => await SendEndBookingEmailToUser(user)
        );
        _logger.LogInformation("An Email has been scheduled to be sent to the user {UserId} at {CheckOutDate}", booking.UserId, booking.CheckOutDate);
    }

    private async Task<UserDTO> GetCorrespondingUser(Guid userId) =>
        await _userRepository.GetByIdAsync(userId);
    private async Task SendEndBookingEmailToUser(UserDTO recipient)
    {
        await _emailService.SendAsync(new EmailDTO
        {
            RecipientEmail = recipient.Email,
            RecipientName = recipient.FirstName,
            Subject = "Your booking has ended",
            Body = $"Your booking For Hotel ........." 
        });

        _logger.LogInformation("Email sent to {RecipientEmail} Regarding his booking", recipient.Email);
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
            throw new KeyNotFoundException("Booking does not exist.");
        }
    }

    public async Task<bool> RoomIsBookedBetween(Guid roomId, DateTime StartingDate, DateTime EndingDate) =>
        await _roomBookingRepository.RoomIsBookedBetween(roomId, StartingDate, EndingDate);

    public async Task<IEnumerable<HotelBookingDTO>> GetByHotelAsync()
    {
        var timeOption = TimeOptions.LastWeek;
        var expression = TimeOptionExpressionBuilder<RoomBooking>.Build(new VisitTimeOptionQuery{ TimeOption = (int)timeOption });
        return await _roomBookingRepository
            .GetAllForHotelsAsync(expression);
    }
}