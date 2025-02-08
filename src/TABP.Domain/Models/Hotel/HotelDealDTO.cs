using TABP.Domain.Abstractions;
using TABP.Domain.Rooms;

namespace TABP.Domain.Hotels;

public class HotelDealDTO : Entity
{
    public string HotelName { get; set; }
    public decimal HotelRating { get; set; }
    public string BriefDescription { get; set; }
    public string CityName { get; set; }
    public IEnumerable<RoomDealDTO> Rooms { get; set; }
}