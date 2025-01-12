using TABP.Domain.Abstractions;

namespace TABP.Domain.Entities;

public class Hotel : Entity
{
    public string Name { get; set; }
    public string BriefDescription { get; set; }
    public string DetailedDescription { get; set; }
    public decimal StarRating { get; set; }
    public string OwnerName { get; set; }
    public string Geolocation { get; set; }
    // do hotel tags thingy
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }

    public Guid CityId { get; set; }
    public City City { get; set; }
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
    public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    public ICollection<HotelReview> HotelReviews { get; set; } = new List<HotelReview>();
    public ICollection<HotelVisit> HotelVisits { get; set; } = new List<HotelVisit>();
}