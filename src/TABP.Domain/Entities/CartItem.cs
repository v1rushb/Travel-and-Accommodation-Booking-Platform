using TABP.Domain.Abstractions;

namespace TABP.Domain.Entities;

public class CartItem : Entity
{
    public DateTime CreationDate { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
}