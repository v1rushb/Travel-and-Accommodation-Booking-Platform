using TABP.Domain.Abstractions;

namespace TABP.Domain.Models.HotelReview;

public class HotelReviewDTO : Entity
{
    public string Feedback { get; set; }
    public decimal Rating { get; set; }
    public bool IsVerifiedPurchase { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }

    public Guid HotelId { get; set; }
    public Guid UserId { get; set; }
}