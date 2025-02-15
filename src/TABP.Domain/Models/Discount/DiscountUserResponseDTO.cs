namespace TABP.Domain.Models.Discount;

public class DiscountUserResponseDTO
{
    public string Reason { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public int AmountPercentage { get; set; }
    public int RoomType { get; set; }

}