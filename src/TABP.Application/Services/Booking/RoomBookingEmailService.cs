using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Abstractions.Services.Booking;
using TABP.Domain.Constants.Email;
using TABP.Domain.Models.Email;
using TABP.Domain.Models.RoomBooking;
using TABP.Domain.Models.User;

namespace TABP.Application.Services.Booking;

public class RoomBookingEmailService : IRoomBookingEmailService
{
    private readonly ILogger<RoomBookingEmailService> _logger;
    private readonly ICacheEventService _cacheEventService;
    private readonly IServiceScopeFactory _scopeFactory;

    public RoomBookingEmailService(
        ILogger<RoomBookingEmailService> logger,
        ICacheEventService cacheEventService,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _cacheEventService = cacheEventService;
        _scopeFactory = scopeFactory;
    }

    public async Task ScheduleSendingBookingEndedEmailJob(RoomBookingDTO booking)
    {
        var user = await GetCorrespondingUser(booking.UserId);
        var timeToSendEmail = booking.CheckOutDate - DateTime.UtcNow;

        await _cacheEventService.ScheduleExpirationAsync(
            Guid.NewGuid().ToString(),
            timeToSendEmail,
            async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IRoomBookingEmailService>();
                await service.SendEndBookingEmailToUserAsync(user, booking);
            }
        );

        _logger.LogInformation(
            "An email has been scheduled to be sent to user {UserId} at {CheckOutDate}.",
            booking.UserId,
            booking.CheckOutDate
        );
    }

    public async Task ScheduleSendingNearEndingBookingEmailJob(RoomBookingDTO booking)
    {
        var user = await GetCorrespondingUser(booking.UserId);
        var timeToSendEmail = (booking.CheckOutDate - DateTime.UtcNow) / 2;

        await _cacheEventService.ScheduleExpirationAsync(
            Guid.NewGuid().ToString(),
            timeToSendEmail,
            async () =>
            {
                using var scope = _scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IRoomBookingEmailService>();
                await service.SendNearEndingBookingEmailAsync(user, booking);
            }
        );

        _logger.LogInformation(
            "An email has been scheduled to be sent to user {UserId} at {Date}.",
            booking.UserId,
            DateTime.UtcNow + timeToSendEmail
        );
    }

    public async Task SendNearEndingBookingEmailAsync(UserDTO recipient, RoomBookingDTO booking)
    {
        var body = await ProcessBookingEmailBodyAsync(
            BookingEmailConstants.Body,
            recipient,
            booking
        );

        await SendEmailAsync(
            recipient,
            body,
            BookingNearEndEmailConstants.Subject
        );

        _logger.LogInformation("Email sent to {RecipientEmail} regarding their booking.", recipient.Email);
    }

    public async Task SendEndBookingEmailToUserAsync(UserDTO recipient, RoomBookingDTO booking)
    {
        var body = await ProcessBookingEmailBodyAsync(
            BookingNearEndEmailConstants.Body,
            recipient,
            booking
        );

        await SendEmailAsync(
            recipient,
            body,
            BookingEmailConstants.Subject
        );

        _logger.LogInformation("Email sent to {RecipientEmail} regarding their booking.", recipient.Email);
    }

    private async Task<string> ProcessBookingEmailBodyAsync(
        string body,
        UserDTO recipient,
        RoomBookingDTO booking)
    {
        using var scope = _scopeFactory.CreateScope();
        var roomService = scope.ServiceProvider.GetRequiredService<IRoomService>();
        var hotelRepository = scope.ServiceProvider.GetRequiredService<IHotelRepository>();

        var room = await roomService.GetByIdAsync(booking.RoomId);
        var hotelName = await hotelRepository.GetHotelNameByIdAsync(room.HotelId);

        return body
            .Replace("{FirstName}", recipient.FirstName)
            .Replace("{HotelName}", hotelName)
            .Replace("{RoomNumber}", room.Number.ToString())
            .Replace("{CheckInDate}", booking.CheckInDate.ToString("MMMM dd, yyyy"))
            .Replace("{CheckOutDate}", booking.CheckOutDate.ToString("MMMM dd, yyyy"))
            .Replace("{TotalPrice}", booking.TotalPrice.ToString("C"));
    }

    private async Task SendEmailAsync(
        UserDTO recipient,
        string body,
        string subject)
    {
        using var scope = _scopeFactory.CreateScope();
        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

        await emailService.SendAsync(new EmailDTO
        {
            RecipientEmail = recipient.Email,
            RecipientName = recipient.FirstName,
            Subject = subject,
            Body = body,
        });

        _logger.LogInformation("Email sent to {RecipientEmail}.", recipient.Email);
    }

    private async Task<UserDTO> GetCorrespondingUser(Guid userId)
    {
        using var scope = _scopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        return await userRepository.GetByIdAsync(userId);
    }
}