using TABP.Domain.Abstractions;

namespace TABP.Domain.Models.City;
public class CityDTO : Entity
{
    public string Name { get; set; }
    public string CountryName { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
}