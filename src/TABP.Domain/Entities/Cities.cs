namespace TABP.Domain.Entities;

public class City : Entity
{
    public string Name { get; set; }
    public string CountryName { get; set; }
    // add timezone, popularity score later and look for other things to add.
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
}