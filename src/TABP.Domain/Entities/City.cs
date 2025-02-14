using TABP.Domain.Abstractions;
using TABP.Domain.Abstractions.Services.Generics;

namespace TABP.Domain.Entities;

/// <summary>
/// Represents a city.
/// </summary>
public class City : Entity, IHasCreationDate
{
    /// <summary>
    /// The name of the city.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The name of the country in which the city is located.
    /// </summary>
    public string CountryName { get; set; }

    /// <summary>
    /// The date and time when the city record was created.
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// The date and time when the city record was last modified.
    /// </summary>
    public DateTime ModificationDate { get; set; }
    
    /// <summary>
    /// Navigation property to a collection of Hotel entities located in this city.
    /// </summary>
    public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}