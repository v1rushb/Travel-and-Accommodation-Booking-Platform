using TABP.Domain.Abstractions;
using TABP.Domain.Enums;
using TABP.Domain.Models.CartItem;

namespace TABP.Domain.Models.Cart;

public class CartDTO : Entity
{
    public Guid UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; } // for now
    public DateTime ModificationDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public DateTime? CreationDate { get; set; }
    public ICollection<CartItemDTO> Items { get; set; } = [];
}