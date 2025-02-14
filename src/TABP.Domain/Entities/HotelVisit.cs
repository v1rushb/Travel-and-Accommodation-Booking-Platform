using TABP.Domain.Abstractions;
using TABP.Domain.Abstractions.Services.Generics;

namespace TABP.Domain.Entities;

/// <summary>
/// Represents a record of a user visiting a hotel.
/// </summary>
public class HotelVisit : Entity, IHasCreationDate
{
    /// <summary>
    /// The date and time when the hotel visit record was created.
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// The unique identifier of the Hotel visited.
    /// </summary>
    public Guid HotelId { get; set; }
    
    /// <summary>
    /// Navigation property to the Hotel entity.
    /// </summary>
    public Hotel Hotel { get; set; }

    /// <summary>
    /// The unique identifier of the User who visited the hotel.
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Navigation property to the User entity.
    /// </summary>
    public User User { get; set; }
}