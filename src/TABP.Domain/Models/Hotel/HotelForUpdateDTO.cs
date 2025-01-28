namespace TABP.Domain.Models.Hotel;

public class HotelForUpdateDTO
{
    public string Name { get; set; }
    public string BriefDescription { get; set; }
    public string DetailedDescription { get; set; }
    public string OwnerName { get; set; }
    public string Geolocation { get; set; }
    public Guid CityId { get; set; }
}