using TABP.Domain.Abstractions;
using TABP.Domain.Abstractions.Services.Generics;

namespace TABP.Domain.Entities;

public class City : Entity, IHasCreationDate
{
    public string Name { get; set; }
    public string CountryName { get; set; }
    // add timezone, popularity score later and look for other things to add.
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }

    public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}