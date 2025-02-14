using TABP.Domain.Abstractions;

namespace TABP.Domain.Entities;

/// <summary>
/// Represents an item within a shopping cart.
/// </summary>
public class CartItem : Entity
{
    /// <summary>
    /// The price of this specific item.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The date and time when this item was added to the cart.
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Any additional notes or special requests for this item.
    /// </summary>
    public string Notes { get; set; }

    /// <summary>
    /// The desired check-in date for this item (e.g., for a hotel room).
    /// </summary>
    public DateTime CheckInDate { get; set; }
    
    /// <summary>
    /// The desired check-out date for this item (e.g., for a hotel room).
    /// </summary>
    public DateTime CheckOutDate { get; set; }

    /// <summary>
    /// The unique identifier of the Room associated with this cart item.
    /// </summary>
    public Guid RoomId { get; set; }

    /// <summary>
    /// Navigation property to the Room entity.
    /// </summary>
    public Room Room { get; set; }

    /// <summary>
    /// The unique identifier of the Cart that contains this item.
    /// </summary>
    public Guid CartId { get; set; }
    
    /// <summary>
    /// Navigation property to the Cart entity.
    /// </summary>
    public Cart Cart { get; set; }
}