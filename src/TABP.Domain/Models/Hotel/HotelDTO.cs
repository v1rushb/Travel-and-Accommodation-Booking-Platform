using TABP.Domain.Abstractions;

namespace TABP.Domain.Models.Hotels;

public class HotelDTO : Entity
{
    public string Name { get; set; }
    public string BriefDescription { get; set; }
    public string DetailedDescription { get; set; }
    public decimal StarRating { get; set; }
    public string OwnerName { get; set; }
    public string Geolocation { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
    public Guid CityId { get; set; }
}