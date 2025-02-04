namespace TABP.Domain.Models.City.Sort;

public class CitySortQuery
{
    public string SortBy { get; set; } = "Name";
    public string SortOrder { get; set; } = "Asc";
    public bool IsAdmin { get; set; } = false;

    public override string ToString() =>
    @$"
            SortBy: {SortBy},
            SortOrder: {SortOrder},
            IsAdmin: {IsAdmin}";
}