using TABP.Domain.Abstractions;
using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

public class Discount : Entity
{
    public string Reason { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public decimal AmountPercentage { get; set; }
    public RoomType roomType { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ModificationDate { get; set; }
    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; }
}