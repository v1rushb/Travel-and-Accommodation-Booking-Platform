using TABP.Domain.Abstractions;

namespace TABP.Domain.Models.Hotel.Search.Response;

public class HotelUserResponseDTO
{
    public string Name { get; set; }
    public string BriefDescription { get; set; }
    public decimal StarRating { get; set; }
}