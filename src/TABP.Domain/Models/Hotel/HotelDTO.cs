using TABP.Domain.Abstractions;
using TABP.Domain.Models.Room;

namespace TABP.Domain.Models.Hotel;

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
    public IEnumerable<RoomDTO> Rooms { get; set; }
    public Guid CityId { get; set; }
}