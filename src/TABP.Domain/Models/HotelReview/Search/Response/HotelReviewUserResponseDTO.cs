using TABP.Domain.Enums;

namespace TABP.Domain.Models.HotelReview.Search.Response;

public class HotelReviewUserResponseDTO
{
    public string Feedback { get; set; }
    public HotelRating Rating { get; set; } // maybe parse?
    public DateTime CreationDate { get; set; }

    public Guid HotelId { get; set; } // maybe add hotel name instead?
}