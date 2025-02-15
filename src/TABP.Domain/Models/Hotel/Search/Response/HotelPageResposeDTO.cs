using TABP.Domain.Abstractions;
using TABP.Domain.Enums;
using TABP.Domain.Models.City.Search;
using TABP.Domain.Models.Discount;

namespace TABP.Domain.Models.Hotel.Search.Response;

public class HotelPageResponseDTO : Entity
{
    public string Name { get; set; }
    public string BriefDescription { get; set; }
    public string DetailedDescription { get; set; }
    public HotelRating StarRating { get; set; }
    public string Geolocation { get; set; }
    public CitySearchResponseDTO City { get; set; }
    public IEnumerable<DiscountUserResponseDTO> Discounts { get; set; }
}