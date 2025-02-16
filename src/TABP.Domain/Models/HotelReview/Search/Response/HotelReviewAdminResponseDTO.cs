using TABP.Domain.Enums;

namespace TABP.Domain.Models.HotelReview.Search.Response;

public class HotelReviewAdminResponseDTO
{
    public string Feedback { get; set; }
    public HotelRating Rating { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
    public Guid HotelId { get; set; }
    public Guid UserId { get; set; }
}