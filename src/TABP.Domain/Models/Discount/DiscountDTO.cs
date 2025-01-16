using TABP.Domain.Abstractions;

namespace TABP.Domain.Models.Discount;
public class DiscountDTO : Entity
    {
        public string Reason { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public decimal AmountPercentage { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public Guid HotelId { get; set; }
    }