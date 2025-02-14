using TABP.Domain.Abstractions;
using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

/// <summary>
/// Represents a review given to a hotel by a user.
/// </summary>
public class HotelReview : Entity
{
    /// <summary>
    /// The text feedback provided by the user in the review.
    /// </summary>
    public string Feedback { get; set; }
    
    /// <summary>
    /// The rating given by the user to the hotel (e.g., Excellent, Good).
    /// </summary>
    public HotelRating Rating { get; set; }
    
    /// <summary>
    /// The date and time when the review was created.
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// The date and time when the review was last modified.
    /// </summary>
    public DateTime ModificationDate { get; set; }

    /// <summary>
    /// The unique identifier of the Hotel being reviewed.
    /// </summary>
    public Guid HotelId { get; set; }

    /// <summary>
    /// Navigation property to the Hotel entity.
    /// </summary>
    public Hotel Hotel { get; set; }

    /// <summary>
    /// The unique identifier of the User who wrote the review.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Navigation property to the User entity.
    /// </summary>
    public User User { get; set; }
}