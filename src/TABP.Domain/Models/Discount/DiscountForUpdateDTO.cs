using TABP.Domain.Enums;

namespace TABP.Domain.Models.Discount;

public class DiscountForUpdateDTO
{
        public string Reason { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public decimal AmountPercentage { get; set; }
        public RoomType roomType { get; set; }
        public Guid HotelId { get; set; }
}