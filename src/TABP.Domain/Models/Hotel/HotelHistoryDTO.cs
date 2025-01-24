namespace TABP.Domain.Models.Hotel;

public class HotelHistoryDTO
{
    public Guid HotelId { get; set; }
    public string Name { get; set; }
    public string BriefDescription { get; set; }
    public decimal StarRating { get; set; }
    public DateTime LastVisitDate { get; set; }
}