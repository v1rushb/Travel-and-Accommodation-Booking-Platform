using TABP.Domain.Enums;

namespace TABP.Domain.Models.Hotel;

public class HotelSearchQuery
{
    public string? SearchTerm { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int NumberOfAdults { get; set; } = 2;
    public int NumberOfChildren { get; set; } = 0;
    public int NumberOfRooms { get; set; }
    public int MinPrice { get; set; }
    public int MaxPrice { get; set; }
    public int? MinStars { get; set; }
    public int? MaxStars { get; set; }
    public string? City { get; set; }
    public IEnumerable<RoomType>? RoomTypes { get; set; }
}