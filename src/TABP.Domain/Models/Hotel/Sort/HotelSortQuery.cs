namespace TABP.Domain.Models.Hotel.Sort;

public class HotelSortQuery
{
    public string SortBy { get; set;} = "Name";
    public string SortOrder { get; set;} = "Asc";
    public bool IsAdmin { get; set; } = false;
}