namespace TABP.Domain.Models.Hotel.Search;

public class HotelSearchQuery
{
    public string? SearchTerm { get; set; }
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public int NumberOfAdults { get; set; } = 2;
    public int NumberOfChildren { get; set; } = 0;
    public int MinNumberOfRooms { get; set; } = 0;
    public int MaxNumberOfRooms { get; set; } = int.MaxValue;
    public int MinPricePerNight { get; set; } = 0;
    public int MaxPricePerNight { get; set; } = int.MaxValue;
    public int? MinStars { get; set; }
    public int? MaxStars { get; set; }
    public string? City { get; set; }
    public IEnumerable<int>? RoomTypes { get; set; }
}