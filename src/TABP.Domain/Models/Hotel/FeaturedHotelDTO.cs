namespace TABP.Domain.Models.Hotel;

public class FeaturedHotelDTO
{
    public string Name { get; set; }
    public Guid HotelId { get; set; }
    public decimal StarRating { get; set; }
    public int Bookings { get; set; }
}