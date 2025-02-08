using TABP.Domain.Abstractions;

namespace TABP.Domain.Rooms;

public class RoomDealDTO : Entity
{
    public string RoomType { get; set; }
    public decimal OriginalPricePerNight { get; set; }
    public decimal FinalPricePerNight { get; set; }
    public DateTime EndDate { get; set; }
    public decimal DiscountPercentage { get; set; }
}