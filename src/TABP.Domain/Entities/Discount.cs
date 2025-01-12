using TABP.Domain.Abstractions;

namespace TABP.Domain.Entities;

public class Discount : Entity
{
    public string Reason { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public decimal AmountPercentage { get; set; } 

    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; }
}