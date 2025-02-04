namespace TABP.Domain.Models.Room.Sort;

public class RoomSortQuery
{
    public string SortBy { get; set;} = "Number";
    public string SortOrder { get; set;} = "Asc";
    public bool IsAdmin { get; set; } = false;

    public override string ToString() =>
        @$"
        SortBy: {SortBy},
        SortOrder: {SortOrder},
        IsAdmin: {IsAdmin}";
}