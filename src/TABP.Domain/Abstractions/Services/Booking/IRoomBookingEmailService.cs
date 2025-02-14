using TABP.Domain.Models.RoomBooking;
using TABP.Domain.Models.User;

namespace TABP.Domain.Abstractions.Services.Booking;

/// <summary>
/// Defines email-related operations for room bookings.
/// This interface outlines methods for scheduling and sending email notifications
/// related to room bookings, such as booking confirmation, reminders, and completion notices.
/// </summary>
public interface IRoomBookingEmailService
{
    /// <summary>
    /// Schedules a job to send an email notification to the user when their booking is nearing its end.
    /// This method automates the process of reminding users about their upcoming check-out date,
    /// enhancing customer service and providing timely information.
    /// </summary>
    /// <param name="booking">
    /// <see cref="RoomBookingDTO"/> representing the booking for which to schedule the near-ending email.
    /// Contains booking details necessary for personalizing the email notification.
    /// </param>
    Task ScheduleSendingNearEndingBookingEmailJob(RoomBookingDTO booking);

    /// <summary>
    /// Schedules a job to send an email notification to the user when their booking has ended.
    /// This method is used to automatically send a follow-up email to users after their stay,
    /// which could include requests for feedback, offers for future bookings, or other relevant information.
    /// </summary>
    /// <param name="booking">
    /// <see cref="RoomBookingDTO"/> representing the booking for which to schedule the booking-ended email.
    /// Provides booking context for the post-stay email notification.
    /// </param>
    Task ScheduleSendingBookingEndedEmailJob(RoomBookingDTO booking);

    /// <summary>
    /// Sends an email notification to the user to remind them that their booking is nearing its end.
    /// This method is responsible for constructing and dispatching the near-ending booking email.
    /// It is typically called by the scheduled job to ensure timely delivery of the notification.
    /// </summary>
    /// <param name="user">
    /// <see cref="UserDTO"/> representing the user who is receiving the email.
    /// Contains user-specific information such as name and email address for email personalization and delivery.
    /// </param>
    /// <param name="booking">
    /// <see cref="RoomBookingDTO"/> representing the booking that is nearing its end.
    /// Provides booking details to include in the email, such as hotel name, room number, and check-out date.
    /// </param>
    Task SendNearEndingBookingEmailAsync(UserDTO user, RoomBookingDTO booking);

    /// <summary>
    /// Sends an email notification to the user to inform them that their booking has ended.
    /// This method handles the creation and sending of the booking-ended email.
    /// It is called by the scheduled job after a booking's check-out date has passed.
    /// </summary>
    /// <param name="user">
    /// <see cref="UserDTO"/> representing the user who is receiving the email.
    /// Contains user details for email communication.
    /// </param>
    /// <param name="booking">
    /// <see cref="RoomBookingDTO"/> representing the booking that has ended.
    /// Supplies booking information to be included in the email, such as booking summary and thank you message.
    /// </param>
    Task SendEndBookingEmailToUserAsync(UserDTO user, RoomBookingDTO booking);
}