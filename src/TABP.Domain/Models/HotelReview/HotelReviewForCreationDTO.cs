namespace TABP.Domain.Models.HotelReview;

public class HotelReviewForCreationDTO
{
    public string Feedback { get; set; }
    public decimal Rating { get; set; }
    public Guid HotelId { get; set; }
}