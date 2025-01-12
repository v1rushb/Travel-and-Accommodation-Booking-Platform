using TABP.Domain.Abstractions;

namespace TABP.Domain.Entities;

// consider doing some enhancements to this entity.
public class HotelVisit : Entity
{
    public DateTime VisitDate { get; set; } // maybe consider using "Date" instead of VisitDate?
    public DateTime TimeSpent { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }

    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}