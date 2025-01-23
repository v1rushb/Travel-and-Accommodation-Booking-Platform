using TABP.Domain.Abstractions;
using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

public class HotelReview : Entity
{
    public string Feedback { get; set; }
    public HotelRating Rating { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }

    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}