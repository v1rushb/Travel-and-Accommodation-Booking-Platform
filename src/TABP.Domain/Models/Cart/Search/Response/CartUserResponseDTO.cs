using TABP.Domain.Enums;
using TABP.Domain.Models.CartItem;

namespace TABP.Domain.Models.Cart.Search.Response;

public class CartUserResponseDTO
{
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public DateTime? CreationDate { get; set; }
    public IEnumerable<CartItemDTO> Items { get; set; }
}