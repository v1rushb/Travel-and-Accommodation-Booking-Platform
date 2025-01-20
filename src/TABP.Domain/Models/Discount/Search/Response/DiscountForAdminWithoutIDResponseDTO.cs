using TABP.Domain.Enums;

namespace TABP.Domain.Models.Discount.Search.Response;

public class DiscountForAdminWithoutIdResponseDTO
{
    public string Reason { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public decimal AmountPercentage { get; set; }
    public RoomType roomType { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
    public Guid HotelId { get; set; }
}