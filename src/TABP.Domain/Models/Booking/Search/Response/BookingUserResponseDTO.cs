using TABP.Domain.Enums;

namespace TABP.Domain.Models.Booking.Search.Response;

public class BookingUserResponseDTO
{
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string Notes { get; set; }
    public BookingStatus Status { get; set; }
    public Guid RoomId { get; set; }
}