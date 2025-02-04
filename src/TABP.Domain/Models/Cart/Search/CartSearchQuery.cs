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

    public override string ToString() =>
    @$"
                    MinTotalPrice: {MinTotalPrice}{(MinTotalPrice == 0 ? " (default)" : "")}, 
                    MaxTotalPrice: {MaxTotalPrice}{(MaxTotalPrice == decimal.MaxValue ? " (default)" : "")}, 
                    Status: {(Status != null && Status.Any() ? string.Join(", ", Status) : "None")}, 
                    MinCheckOutDate: {MinCheckOutDate?.ToString("yyyy-MM-dd") ?? "None"}, 
                    MaxCheckOutDate: {MaxCheckOutDate?.ToString("yyyy-MM-dd") ?? "None"}, 
                    MinCreationDate: {MinCreationDate?.ToString("yyyy-MM-dd") ?? "None"}, 
                    MaxCreationDate: {MaxCreationDate?.ToString("yyyy-MM-dd") ?? "None"}";

}