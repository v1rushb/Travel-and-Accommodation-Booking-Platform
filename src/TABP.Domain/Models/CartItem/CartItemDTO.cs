using TABP.Domain.Abstractions;

namespace TABD.Domain.Models.CartItem;

public class CartItemDTO : Entity
{
    public DateTime CreationDate { get; set; }
    public Guid UserId { get; set; }
    public Guid RoomId { get; set; }
}