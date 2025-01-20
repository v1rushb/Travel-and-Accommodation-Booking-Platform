using TABP.Domain.Enums;

namespace TABP.Domain.Models.Room.Search;

public class RoomSearchQuery
{
    public int Number { get; set; }
    public RoomType Type { get; set; }
    public int AdultsCapacity { get; set; } = 2;
    public int ChildrenCapacity { get; set; } = 0;
    public int PricePerNight { get; set; }
}