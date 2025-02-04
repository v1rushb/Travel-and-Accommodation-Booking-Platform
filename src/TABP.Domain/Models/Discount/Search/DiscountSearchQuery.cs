
using TABP.Domain.Enums;

namespace TABP.Domain.Models.Discount.Search;

public class DiscountSearchQuery
{
    public string SearchTerm { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public decimal MinAmountPercentage { get; set; } = 0;
    public decimal MaxAmountPercentage { get; set; } = 100;
    public IEnumerable<int> RoomType { get; set; }

    public override string ToString() =>
    @$"
                    SearchTerm: {SearchTerm ?? "default"},
                    StartingDate: {StartingDate.ToString("yyyy-MM-dd") ?? "default"},
                    EndingDate: {EndingDate.ToString("yyyy-MM-dd") ?? "default"},
                    MinAmountPercentage: {MinAmountPercentage}{(MinAmountPercentage == 0 ? " (default)" : "")}, 
                    MaxAmountPercentage: {MaxAmountPercentage}{(MaxAmountPercentage == 100 ? " (default)" : "")},   
                    RoomType: {(RoomType != null && RoomType.Any() ? string.Join(", ", RoomType) : "default")}";
}