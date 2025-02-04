namespace TABP.Domain.Models.Hotel.Sort;

public class HotelSortQuery
{
    public string SortBy { get; set;} = "Name";
    public string SortOrder { get; set;} = "Asc";
    public bool IsAdmin { get; set; } = false;

    public override string ToString() =>
        @$"
                SortBy: {SortBy},
                SortOrder: {SortOrder},
                IsAdmin: {IsAdmin}";
}