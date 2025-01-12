using TABP.Domain.Abstractions;

namespace TABP.Domain.Entities;

public class User : Entity
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public DateTime LastLogin { get; set; }

    public ICollection<Role> Roles { get; set; } = new List<Role>();
    public ICollection<RoomBooking> RoomBookings { get; set; } = new List<RoomBooking>();
    public ICollection<HotelReview> HotelReviews { get; set; } = new List<HotelReview>();
    public ICollection<HotelVisit> HotelVisits { get; set; } = new List<HotelVisit>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

}