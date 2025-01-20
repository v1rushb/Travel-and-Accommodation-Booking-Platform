using TABP.Domain.Enums;

namespace TABP.Domain.Models.HotelReview;

public class HotelReviewForUpdateDTO
{
    public string Feedback { get; set; }
    public HotelRating Rating { get; set; }
    public bool IsVerifiedPurchase { get; set; }
    public Guid HotelId { get; set; }
    public Guid UserId { get; set; }
}