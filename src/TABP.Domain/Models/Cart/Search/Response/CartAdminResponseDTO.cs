using TABP.Domain.Enums;

namespace TABP.Domain.Models.Cart.Search.Response;

public class CartAdminResponseDTO
{
    public Guid UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; } // for now
    public DateTime ModificationDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public DateTime? CreationDate { get; set; }
}