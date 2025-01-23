namespace TABP.Domain.Models.Hotel;

public class VisitedHotelDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Visits { get; set; }
    public decimal StarRating { get; set; }
}