using TABP.Domain.Enums;

namespace TABP.Domain.Models.Room.Search.Response;

public class RoomForAdminWithoutIdDTO
{
    public int Number { get; set; }
    public RoomType Type { get; set; }
    public int AdultsCapacity { get; set; }
    public int ChildrenCapacity { get; set; }
    public int PricePerNight { get; set; }
    public Guid HotelId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
}