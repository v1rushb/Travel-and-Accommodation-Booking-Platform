using TABP.Domain.Abstractions;
using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

public class Cart : Entity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public DateTime CreationDate { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; } // for now.
    public DateTime ModificationDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
}