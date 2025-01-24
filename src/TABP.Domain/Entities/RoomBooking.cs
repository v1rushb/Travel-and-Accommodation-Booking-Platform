using TABP.Domain.Abstractions;
using TABP.Domain.Abstractions.Services.Generics;
using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

public class RoomBooking : Entity, IHasCreationDate
{
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
    public string Notes { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }

    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}