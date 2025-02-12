using TABP.Domain.Entities;
using TABP.Domain.Models.RoomBooking;
using TABP.Domain.Models.User;

namespace TABP.Domain.Abstractions.Services.Booking;

public interface IRoomBookingEmailService
{
    Task ScheduleSendingNearEndingBookingEmailJob(RoomBookingDTO booking);
    Task ScheduleSendingBookingEndedEmailJob(RoomBookingDTO booking);
    Task SendNearEndingBookingEmailAsync(UserDTO user, RoomBookingDTO booking);
    Task SendEndBookingEmailToUserAsync(UserDTO user, RoomBookingDTO booking);
}