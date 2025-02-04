namespace TABP.Domain.Models.HotelReview.Sort;

public class ReviewSortQuery
{
    public string SortBy { get; set; } = "Rating";
    public string SortOrder { get; set; } = "Asc";
    public bool IsAdmin { get; set; } = false;

    public override string ToString() =>
    @$"
                    SortBy: {SortBy},
                    SortOrder: {SortOrder},
                    IsAdmin: {IsAdmin}";
}