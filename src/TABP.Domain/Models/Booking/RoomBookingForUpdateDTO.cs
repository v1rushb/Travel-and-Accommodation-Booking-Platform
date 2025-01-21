namespace TABP.Domain.Models.Booking;

public class RoomBookingForUpdateDTO
{
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string Notes { get; set; }
    public Guid RoomId { get; set; }
}