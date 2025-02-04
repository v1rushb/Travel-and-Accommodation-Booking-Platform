namespace TABP.Domain.Models.Cart.Sort;

public class CartSortQuery
{
    public string SortBy { get; set; } = "TotalPrice";
    public string SortOrder { get; set; } = "Asc";
    public bool IsAdmin { get; set; } = false;

    public override string ToString() =>
    @$"
                    SortBy: {SortBy},
                    SortOrder: {SortOrder},
                    IsAdmin: {IsAdmin}";
}