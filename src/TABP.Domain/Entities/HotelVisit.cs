using TABP.Domain.Abstractions;
using TABP.Domain.Abstractions.Services.Generics;

namespace TABP.Domain.Entities;

// consider doing some enhancements to this entity.
public class HotelVisit : Entity, IHasCreationDate
{
    public DateTime CreationDate { get; set; }
    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}