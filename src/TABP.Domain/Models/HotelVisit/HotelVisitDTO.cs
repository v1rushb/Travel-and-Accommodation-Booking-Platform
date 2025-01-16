using TABP.Domain.Abstractions;

namespace TABP.Domain.Models.HotelVisit;

public class HotelVisitDTO : Entity
{
    public DateTime Date { get; set; }
    public TimeSpan TimeSpent { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }

    public Guid HotelId { get; set; }
    public Guid UserId { get; set; }
}