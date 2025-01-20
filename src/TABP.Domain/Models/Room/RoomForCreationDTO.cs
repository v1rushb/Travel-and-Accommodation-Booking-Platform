using TABP.Domain.Enums;

namespace TABP.Domain.Models.Room;

public class RoomForCreationDTO
{
    public RoomType Type { get; set; }
    public int AdultsCapacity { get; set; }
    public int ChildrenCapacity { get; set; }
    public int PricePerNight { get; set; }
    public Guid HotelId { get; set; }
}