using TABP.Domain.Abstractions;

public class AvailableRoom : Entity
{
    public int Number { get; set; }
    public int Type { get; set; }
    public int AdultsCapacity { get; set; }
    public int ChildrenCapacity { get; set; }
    public int PricePerNight { get; set; }
    public Guid HotelId { get; set; }
    public string HotelName { get; set; }
    public decimal StarRating { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
}