namespace TABP.Domain.Models.Room.Search;

public class RoomSearchQuery
{
    public int MinNumber { get; set; } = 1;
    public int MaxNumber { get; set; } = int.MaxValue;
    public IEnumerable<int> roomType { get; set; } 
    public int MinAdultsCapacity { get; set; } = 2;
    public int MaxAdultsCapacity { get; set; } = int.MaxValue;
    public int MinChildrenCapacity { get; set; } = 0;
    public int MaxChildrenCapacity { get; set; } = int.MaxValue;
    public int MinPricePerNight { get; set; } = 0;
    public int MaxPricePerNight { get; set; } = int.MaxValue;
}