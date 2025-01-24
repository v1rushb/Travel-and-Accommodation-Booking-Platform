using TABP.Domain.Abstractions;
using TABP.Domain.Models.Hotels;

namespace TABP.Domain.Models.City.Search;

public class CitySearchResponseDTO : Entity
{
    public string Name { get; set; }
    public string CountryName { get; set; }
}