using TABP.Domain.Abstractions;
using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

/// <summary>
/// Represents a room in a hotel.
/// </summary>
public class Room : Entity
{
    /// <summary>
    /// The room number within the hotel.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// The type of the room (e.g., Single, Double, Suite).
    /// </summary>
    public RoomType Type { get; set; }

    /// <summary>
    /// The maximum number of adults the room can accommodate.
    /// </summary>
    public int AdultsCapacity { get; set; }

    /// <summary>
    /// The maximum number of children the room can accommodate.
    /// </summary>
    public int ChildrenCapacity { get; set; }

    /// <summary>
    /// The price per night for staying in this room.
    /// </summary>
    public int PricePerNight { get; set; }

    /// <summary>
    /// The date and time when the room record was created.
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// The date and time when the room record was last modified.
    /// </summary>
    public DateTime ModificationDate { get; set; }

    /// <summary>
    /// The unique identifier of the Hotel this room belongs to.
    /// </summary>
    public Guid HotelId { get; set; }

    /// <summary>
    /// Navigation property to the Hotel entity.
    /// </summary>
    public Hotel Hotel { get; set; }

    /// <summary>
    /// Navigation property to a collection of RoomBooking entities for this room.
    /// </summary>
    public ICollection<RoomBooking> RoomBookings { get; set; } = new List<RoomBooking>();
    
    /// <summary>
    /// Navigation property to a collection of CartItem entities related to this room.
    /// </summary>
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}