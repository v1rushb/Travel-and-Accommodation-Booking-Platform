using TABP.Domain.Abstractions;

namespace TABP.Domain.Entities;

/// <summary>
/// Represents a user of the application.
/// </summary>
public class User : Entity
{
    /// <summary>
    /// The unique username for login.
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// The user's password (should be hashed in a real application).
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// The user's first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// The user's last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// The user's email address.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// The date and time of the user's last login.
    /// </summary>
    public DateTime LastLogin { get; set; }

    /// <summary>
    /// Navigation property to a collection of Role entities assigned to this user.
    /// </summary>
    public ICollection<Role> Roles { get; set; } = new List<Role>();

    /// <summary>
    /// Navigation property to a collection of RoomBooking entities made by this user.
    /// </summary>
    public ICollection<RoomBooking> RoomBookings { get; set; } = new List<RoomBooking>();

    /// <summary>
    /// Navigation property to a collection of HotelReview entities written by this user.
    /// </summary>
    public ICollection<HotelReview> HotelReviews { get; set; } = new List<HotelReview>();

    /// <summary>
    /// Navigation property to a collection of HotelVisit entities representing hotels visited by this user.
    /// </summary>
    public ICollection<HotelVisit> HotelVisits { get; set; } = new List<HotelVisit>();

     /// <summary>
    /// Navigation property to a collection of CartItem entities created by this user.
    /// </summary>
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}