namespace TABP.Domain.Models.HotelReview.Search;

public class AdminReviewSearchQuery
{
    public string? SearchTerm { get; set; }
    public IEnumerable<int>? Rating { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid? HotelId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? Id { get; set; } = null;

    public override string ToString()
    {
        var searchTermDisplay = !string.IsNullOrEmpty(SearchTerm) 
            ? $"'{SearchTerm}'" 
            : "None";

        return @$"
                    SearchTerm: {searchTermDisplay},
                    Rating: {(Rating != null ? string.Join(", ", Rating) : "None")},
                    CreationDate: {CreationDate},
                    StartDate: {StartDate?.ToString("yyyy-MM-dd") ?? "None"},
                    EndDate: {EndDate?.ToString("yyyy-MM-dd") ?? "None"},
                    HotelId: {GetHotelIdStateString()}, 
                    UserId: {UserId?.ToString() ?? "None"}
                    Id: {GetIdStateString()}";
        }

    private string GetIdStateString() =>
        Id.HasValue ? Id.Value.ToString() : "None";

    private string GetHotelIdStateString() =>
        HotelId.HasValue ? HotelId.Value.ToString() : "None";

}