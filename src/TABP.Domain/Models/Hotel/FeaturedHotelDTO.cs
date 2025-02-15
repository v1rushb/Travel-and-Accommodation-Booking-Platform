using TABP.Domain.Abstractions;

namespace TABP.Domain.Models.Hotel;

public class FeaturedHotelDTO : Entity
{
    public string Name { get; set; }
    public decimal StarRating { get; set; }
    public int WeeklyBookings { get; set; }
    public int WeeklyVisits { get; set; }
    public int UniqueVisitors { get; set; }
}