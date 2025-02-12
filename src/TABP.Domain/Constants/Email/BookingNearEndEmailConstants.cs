namespace TABP.Domain.Constants.Email;

public static class BookingNearEndEmailConstants
{
    public const string Subject = "Your Booking is About to End";
    public const string Body = @"
        Dear {FirstName},

        Thank you for choosing our service. We would like to note you that your booking with details:

        Hotel: {HotelName}
        Room Number: {RoomNumber}

        Will end tomorrow on {CheckOutDate}, so be advised.

        If you have any questions or feedback, please feel free to reach out to our support team.

        Best regards,
        The noice Team
        ";
}