namespace TABP.Domain.Constants.Email;

public static class BookingEmailConstants
{
    public const string Subject = "Your Booking Has Ended - Thank You for Staying With Us!";
    public const string Body = @"
        Dear {FirstName},

        Thank you for choosing our service. We hope you had a pleasant stay. Here are the details of your recent booking:

        Hotel: {HotelName}
        Room ID: {RoomNumber}
        Check-In Date: {CheckInDate}
        Check-Out Date: {CheckOutDate}
        Total Price: {TotalPrice}

        If you have any questions or feedback, please feel free to reach out to our support team.

        Best regards,
        The Booking Team
        ";
}