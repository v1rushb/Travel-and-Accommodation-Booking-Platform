using TABP.Domain.Enums;

namespace TABP.Domain.Models.HotelReview.Search;

public class ReviewSearchQuery
{

    public string SearchTerm { get; set; }
    public IEnumerable<int>? Rating { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid HotelId { get; set; }

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
                HotelId: {HotelId}";
    }

}