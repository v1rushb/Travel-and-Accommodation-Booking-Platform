using TABP.Domain.Abstractions;
using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

/// <summary>
/// Represents a discount offered by a hotel.
/// </summary>
public class Discount : Entity
{
    /// <summary>
    /// The reason or description for the discount.
    /// </summary>
    public string Reason { get; set; }

    /// <summary>
    /// The date and time when the discount becomes active.
    /// </summary>
    public DateTime StartingDate { get; set; }

    /// <summary>
    /// The date and time when the discount expires.
    /// </summary>
    public DateTime EndingDate { get; set; }

    /// <summary>
    /// The discount amount as a percentage (e.g., 10 for 10%).
    /// </summary>
    public decimal AmountPercentage { get; set; }

    /// <summary>
    /// The type of room this discount applies to.
    /// </summary>
    public RoomType roomType { get; set; }

    /// <summary>
    /// The date and time when the discount record was created.
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// The date and time when the discount record was last modified.
    /// </summary>
    public DateTime ModificationDate { get; set; }

    /// <summary>
    /// The unique identifier of the Hotel offering this discount.
    /// </summary>
    public Guid HotelId { get; set; }

    /// <summary>
    /// Navigation property to the Hotel entity.
    /// </summary>
    public Hotel Hotel { get; set; }
}