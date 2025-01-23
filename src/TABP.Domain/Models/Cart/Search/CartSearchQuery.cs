namespace TABP.Domain.Models.Cart.Search;

public class CartSearchQuery
{
    public decimal MinTotalPrice { get; set; } = 0;
    public decimal MaxTotalPrice { get; set; } = decimal.MaxValue;
    public IEnumerable<int>? Status { get; set; } // for now
    public DateTime? MinCheckOutDate { get; set; }
    public DateTime? MaxCheckOutDate { get; set; }
    public DateTime? MinCreationDate { get; set; }
    public DateTime? MaxCreationDate { get; set; }

}