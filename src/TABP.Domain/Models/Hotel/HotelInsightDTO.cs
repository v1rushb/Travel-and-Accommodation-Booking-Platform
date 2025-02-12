
using TABP.Domain.Abstractions.Services.Generics;
using TABP.Domain.Models.Hotel;

namespace TABP.Domain.Models.Hotels;

public class HotelInsightDTO : HotelDTO, IHasCreationDate
{
    public decimal Revenue { get; set; }
    public string CityName { get; set; }
} 