namespace TABP.Domain.Models.RoomBooking;

public class RoomBookingForCreationDTO
{
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string Notes { get; set; }
    public Guid RoomId { get; set; }
}
