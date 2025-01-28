using TABP.Domain.Abstractions;

namespace TABP.Domain.Models.CartItem;

public class CartItemUserResponseDTO : Entity
{
    public decimal Price { get; set; }
    public string Notes { get; set; }
    public DateTime CreationDate { get; set; }
    public Guid RoomId { get; set; }
    public Guid CartId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
}