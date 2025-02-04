namespace TABP.Domain.Models.RoomBooking;

public class BookingSortQuery
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