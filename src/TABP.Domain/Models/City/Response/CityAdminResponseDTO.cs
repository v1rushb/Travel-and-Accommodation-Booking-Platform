using TABP.Domain.Abstractions;

namespace TABP.Domain.Models.City.Response;

public class CityAdminResponseDTO : Entity
{
    public string Name { get; set; }
    public string CountryName { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
    // later: do the query options : includeHotels=true
}