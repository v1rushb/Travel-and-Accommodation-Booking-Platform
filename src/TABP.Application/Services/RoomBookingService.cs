using System.Linq.Expressions;
using System.Timers;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Application.Filters.ExpressionBuilders.Generics;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Models.Booking;
using TABP.Domain.Models.Booking.Search;
using TABP.Domain.Models.Booking.Search.Response;
using TABP.Domain.Models.Cart;
using TABP.Domain.Models.Discount;
using TABP.Domain.Models.Email;
using TABP.Domain.Models.HotelVisit;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Room;
using TABP.Domain.Models.RoomBooking;
using TABP.Domain.Models.User;

namespace TABP.Application.Services;

public class RoomBookingService : IRoomBookingService
{
    private readonly IRoomBookingRepository _roomBookingRepository;
    private readonly ILogger<RoomBookingService> _logger;
    private readonly IValidator<RoomBookingDTO> _bookingValidator;
    private readonly ICurrentUserService _currentUserService;
    private readonly IDiscountRepository _discountRepository;
    private readonly IRoomService _roomService;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;
    private readonly ICacheEventService _cacheEventService;
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepository;

    public RoomBookingService(
        IRoomBookingRepository roomBookingRepository,
        ILogger<RoomBookingService> logger,
        ICurrentUserService currentUserService,
        IDiscountRepository discountRepository,
        IRoomService roomService,
        IValidator<RoomBookingDTO> bookingValidator,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper,
        ICacheEventService cacheEventService,
        IEmailService emailService,
        IUserRepository userRepository)
    {
        _roomBookingRepository = roomBookingRepository;
        _logger = logger;
        _currentUserService = currentUserService;
        _discountRepository = discountRepository;
        _roomService = roomService;
        _bookingValidator = bookingValidator;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
        _cacheEventService = cacheEventService;
        _emailService = emailService;
        _userRepository = userRepository;
    }
    
    [Obsolete]
    public async Task<Guid> AddAsync(RoomBookingDTO newBooking)
    {
        var currentUserId = _currentUserService.GetUserId();
        newBooking.UserId = currentUserId;


        await _bookingValidator.ValidateAndThrowAsync(newBooking);

        newBooking.CreationDate = DateTime.UtcNow;
        newBooking.ModificationDate = DateTime.UtcNow;
        newBooking.Status = BookingStatus.Confirmed;

        var room = await _roomService.GetByIdAsync(newBooking.RoomId);

        var discount = await _discountRepository.GetHighestDiscountActiveForHotelRoomTypeAsync(room.HotelId, room.Type);
        
        SetFinalTotalPrice(newBooking, room, discount);

        var bookingId = await _roomBookingRepository.AddAsync(newBooking);
    
        _logger.LogInformation("Booking with Id: {Id} has been added to User {UserId}, with Discount {Discount}%", bookingId, currentUserId, discount.AmountPercentage);
        
        return bookingId;
    }

    public async Task AddAsync(CartDTO cart)
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
                Status = BookingStatus.Confirmed, // remove later.
                CreationDate = DateTime.UtcNow,
                ModificationDate = DateTime.UtcNow,
                Notes = item.Notes,
            };

            // await _bookingValidator.ValidateAndThrowAsync(booking);

            bookings.Add(booking);
        }

        foreach(var booking in bookings)
        {
            var room = await _roomService.GetByIdAsync(booking.RoomId);
            var discount = await _discountRepository.GetHighestDiscountActiveForHotelRoomTypeAsync(room.HotelId, room.Type);
            SetFinalTotalPrice(booking, room, discount);
            _logger.LogInformation("Booking with Id: {Id} has been added to User {UserId}, with Discount {Discount}%", booking.Id, cart.UserId, discount.AmountPercentage);
            await SchduleSendingBookingEndedEmailJob(booking);
        }
        await _roomBookingRepository.AddAsync(bookings); // just adds range of bookings.
    }

    private async Task SchduleSendingBookingEndedEmailJob(RoomBookingDTO booking)
    {
        var user = await GetCorrespondingUser(booking);
        var timeToSendEmail = booking.CheckOutDate - DateTime.UtcNow;
        await _cacheEventService.ScheduleExpirationAsync(
            new Guid().ToString(),
            timeToSendEmail,
            async () => await SendEndBookingEmailToUser(user, booking.Id)
        );
    }

    private async Task<UserDTO> GetCorrespondingUser(RoomBookingDTO booking) =>
        await _userRepository.GetByIdAsync(booking.UserId);
    private async Task SendEndBookingEmailToUser(UserDTO recipient, Guid bookingId)
    {
        await _emailService.SendAsync(new EmailDTO
        {
            RecipientEmail = recipient.Email,
            RecipientName = recipient.FirstName,
            Subject = "Your booking has ended",
            Body = $"Your booking with Id {bookingId} has ended."
        });
    }

    [Obsolete]
    public async Task DeleteAsync(Guid Id)
    {
        var currentUserId = _currentUserService.GetUserId();

        await ValidateOwnership(Id, currentUserId);

        await _roomBookingRepository.DeleteAsync(Id);
        _logger.LogInformation("Booking with Id: {Id} has been deleted from User {UserId}", Id, currentUserId);
    }

    public async Task<RoomBookingDTO> GetByIdAsync(Guid Id) =>
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

    [Obsolete]
    public async Task UpdateAsync(RoomBookingDTO updatedBooking)
    {
        await ValidateId(updatedBooking.Id); // do more proper validation.

        var currentUserId = _currentUserService.GetUserId();
        var booking = await GetByIdAsync(updatedBooking.Id);
        
        await ValidateOwnership(booking.Id, currentUserId);

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

    public async Task<IEnumerable<BookingUserResponseDTO>> SearchUserBookingsAsync(
        BookingSearchQuery query,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var currentUserId = _currentUserService.GetUserId();
        var expression = BookingExpressionBuilder.Build(query, currentUserId);

        return await _roomBookingRepository.SearchUserBookingsAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);
    }

    public async Task<IEnumerable<BookingAdminResponseDTO>> SearchAdminAsync(
        AdminBookingSearchQuery inQuery,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var userId = inQuery.UserId;
        var query = _mapper.Map<BookingSearchQuery>(inQuery);
        
        var expression = BookingExpressionBuilder.Build(query, userId);

        return await _roomBookingRepository.SearchAdminAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);
    }

    public async Task<IEnumerable<HotelBookingDTO>> GetByHotelAsync()
    {
        var timeOption = TimeOptions.LastWeek;
        var expression = TimeOptionExpressionBuilder<RoomBooking>.Build(new VisitTimeOptionQuery{ TimeOption = (int)timeOption });
        return await _roomBookingRepository
            .GetAllForHotelsAsync(expression);

    }
}