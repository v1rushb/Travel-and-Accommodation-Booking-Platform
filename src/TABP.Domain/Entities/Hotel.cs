using TABP.Domain.Abstractions;
using TABP.Domain.Abstractions.Services.Generics;

namespace TABP.Domain.Entities;

/// <summary>
/// Represents a hotel.
/// </summary>
public class Hotel : Entity, IHasCreationDate
{
    /// <summary>
    /// The name of the hotel.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// A short description of the hotel.
    /// </summary>
    public string BriefDescription { get; set; }

    /// <summary>
    /// A more detailed description of the hotel and its amenities.
    /// </summary>
    public string DetailedDescription { get; set; }

    /// <summary>
    /// The star rating of the hotel (e.g., 3 stars, 5 stars).
    /// </summary>
    public decimal StarRating { get; set; }

    /// <summary>
    /// The name of the hotel's owner or manager.
    /// </summary>
    public string OwnerName { get; set; }

    /// <summary>
    /// Geographical location information of the hotel (e.g., coordinates, address).
    /// </summary>
    public string Geolocation { get; set; }

    /// <summary>
    /// The date and time when the hotel record was created.
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// The date and time when the hotel record was last modified.
    /// </summary>
    public DateTime ModificationDate { get; set; }

    /// <summary>
    /// The unique identifier of the City where this hotel is located.
    /// </summary>
    public Guid CityId { get; set; }

    /// <summary>
    /// Navigation property to the City entity.
    /// </summary>
    public City City { get; set; }

    /// <summary>
    /// Navigation property to a collection of Room entities in this hotel.
    /// </summary>
    public ICollection<Room> Rooms { get; set; } = new List<Room>();

    /// <summary>
    /// Navigation property to a collection of Discount entities offered by this hotel.
    /// </summary>
    public ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    /// <summary>
    /// Navigation property to a collection of HotelReview entities for this hotel.
    /// </summary>
    public ICollection<HotelReview> HotelReviews { get; set; } = new List<HotelReview>();
    
    /// <summary>
    /// Navigation property to a collection of HotelVisit entities for this hotel.
    /// </summary>
    public ICollection<HotelVisit> HotelVisits { get; set; } = new List<HotelVisit>();
}