using TABP.Domain.Abstractions;
using TABP.Domain.Enums;

namespace TABP.Domain.Models.RoomBooking;

public class RoomBookingDTO : Entity
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
}
