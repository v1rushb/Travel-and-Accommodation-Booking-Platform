using TABP.Domain.Abstractions;

namespace TABP.Domain.Entities;

public class CartItem : Entity
{
    public decimal Price { get; set; }
    public DateTime CreationDate { get; set; }
    public string Notes { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    public Guid CartId { get; set; }
    public Cart Cart { get; set; }

}