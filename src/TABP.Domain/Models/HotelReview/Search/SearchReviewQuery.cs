using TABP.Domain.Enums;

namespace TABP.Domain.Models.HotelReview.Search;

public class ReviewSearchQuery
{

    public string SearchTerm { get; set; }
    public HotelRating Rating { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid HotelId { get; set; }
}