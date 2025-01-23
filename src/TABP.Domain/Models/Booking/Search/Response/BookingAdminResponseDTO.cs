using TABP.Domain.Enums;

namespace TABP.Domain.Models.Booking.Search.Response;

public class BookingAdminResponseDTO
{
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string Notes { get; set; }
    public BookingStatus Status { get; set; }
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
    // probably add cart items here.
}