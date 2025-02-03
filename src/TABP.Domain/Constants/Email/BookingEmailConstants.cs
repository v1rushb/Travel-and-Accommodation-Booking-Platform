namespace TABP.Domain.Constants.Email;

public static class BookingEmailConstants
{
    public const string Subject = "Your Booking Has Ended - Thank You for Staying With Us!";
    public const string Body = @"
        Dear {recipient.FirstName},

        Thank you for choosing our service. We hope you had a pleasant stay. Here are the details of your recent booking:

        Hotel: {hotelName}
        Room ID: {booking.RoomId}
        Check-In Date: {booking.CheckInDate:MMMM dd, yyyy}
        Check-Out Date: {booking.CheckOutDate:MMMM dd, yyyy}
        Total Price: {booking.TotalPrice:C}

        If you have any questions or feedback, please feel free to reach out to our support team.

        Best regards,
        The Booking Team
        ";
}