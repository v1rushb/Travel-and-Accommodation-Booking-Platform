using TABP.Domain.Enums;

namespace TABP.Domain.Models.HotelReview;

public class HotelReviewForCreationDTO
{
    public string Feedback { get; set; }
    public decimal Rating { get; set; }
    public bool IsVerifiedPurchase { get; set; }
    public Guid HotelId { get; set; }
}