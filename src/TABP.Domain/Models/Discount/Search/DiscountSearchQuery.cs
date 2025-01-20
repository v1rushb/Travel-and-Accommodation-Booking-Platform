
using TABP.Domain.Enums;

namespace TABP.Domain.Models.Discount.Search;

public class DiscountSearchQuery
{
    public string SearchTerm { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public decimal AmountPercentage { get; set; }
    public RoomType roomType { get; set; }
}