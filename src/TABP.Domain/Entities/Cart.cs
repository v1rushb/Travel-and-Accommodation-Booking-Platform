using TABP.Domain.Abstractions;
using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

/// <summary>
/// Represents a shopping cart for a user.
/// </summary>
public class Cart : Entity
{
    /// <summary>
    /// The unique identifier of the user who owns this cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Navigation property to the User entity.
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// The date and time when the cart was created.
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// The total price of all items in the cart.
    /// </summary>
    public decimal TotalPrice { get; set; }
    
    /// <summary>
    /// The current status of the booking associated with this cart (e.g., Pending, Confirmed).
    /// </summary>
    public BookingStatus Status { get; set; }

    /// <summary>
    /// The date and time when the cart was last modified.
    /// </summary>
    public DateTime ModificationDate { get; set; }

    /// <summary>
    /// The intended check-out date for the booking. Nullable if not yet specified.
    /// </summary>
    public DateTime? CheckOutDate { get; set; }

    /// <summary>
    /// Navigation property to a collection of CartItem entities in this cart.
    /// </summary>
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
}