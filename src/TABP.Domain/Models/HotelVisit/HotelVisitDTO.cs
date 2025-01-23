using TABP.Domain.Abstractions;

namespace TABP.Domain.Models.HotelVisit;

public class HotelVisitDTO : Entity
{
    public DateTime CreationDate { get; set; }
    public Guid HotelId { get; set; }
    public Guid UserId { get; set; }
}