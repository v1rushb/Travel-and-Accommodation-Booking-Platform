using TABP.Domain.Abstractions;
using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

public class RoomBooking : Entity
{
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public decimal Price { get; set; }
    public BookingStatus Status { get; set; }
    public string Notes { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }

}