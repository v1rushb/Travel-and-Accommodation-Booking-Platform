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
}