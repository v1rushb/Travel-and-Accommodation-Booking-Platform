using TABP.Domain.Abstractions;
using TABP.Domain.Abstractions.Services.Generics;
using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

/// <summary>
/// Represents a booking of a room by a user.
/// </summary>
public class RoomBooking : Entity, IHasCreationDate
{
    /// <summary>
    /// The date when the booking period starts.
    /// </summary>
    public DateTime CheckInDate { get; set; }

    /// <summary>
    /// The date when the booking period ends.
    /// </summary>
    public DateTime CheckOutDate { get; set; }

    /// <summary>
    /// The total price for the entire booking period.
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// The current status of the room booking (e.g., Pending, Confirmed, Cancelled).
    /// </summary>
    public BookingStatus Status { get; set; }

    /// <summary>
    /// Any special notes or requests associated with the booking.
    /// </summary>
    public string Notes { get; set; }

    /// <summary>
    /// The date and time when the booking record was created.
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// The date and time when the booking record was last modified.
    /// </summary>
    public DateTime ModificationDate { get; set; }

    /// <summary>
    /// The unique identifier of the Room being booked.
    /// </summary>
    public Guid RoomId { get; set; }

    /// <summary>
    /// Navigation property to the Room entity.
    /// </summary>
    public Room Room { get; set; }

    /// <summary>
    /// The unique identifier of the User who made the booking.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Navigation property to the User entity.
    /// </summary>
    public User User { get; set; }
}