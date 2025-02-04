namespace TABP.Domain.Models.Discount.Sort;

public class DiscountSortQuery
{
    public string SortBy { get; set; } = "AmountPercentage";
    public string SortOrder { get; set; } = "Asc";
    public bool IsAdmin { get; set; } = false;

    public override string ToString() =>
    @$"
                    SortBy: {SortBy},
                    SortOrder: {SortOrder},
                    IsAdmin: {IsAdmin}";
}