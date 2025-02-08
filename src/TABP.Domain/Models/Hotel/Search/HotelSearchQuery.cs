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
    public Guid? Id { get; set; } = null;

    public override string ToString()
    {
        var searchTermDisplay = !string.IsNullOrEmpty(SearchTerm) 
        ? $"'{SearchTerm}'" 
        : "None";

        return @$"
                    SearchTerm: {searchTermDisplay},
                    CheckInDate: {CheckInDate?.ToString("yyyy-MM-dd") ?? "None"} - CheckOutDate: {CheckOutDate?.ToString("yyyy-MM-dd") ?? "None"},
                    NumberOfAdults: {NumberOfAdults}{(NumberOfAdults == 2 ? " (default)" : "")}, 
                    NumberOfChildren: {NumberOfChildren}{(NumberOfChildren == 0 ? " (default)" : "")}, 
                    MinNumberOfRooms: {MinNumberOfRooms}{(MinNumberOfRooms == 0 ? " (default)" : "")}, 
                    MaxNumberOfRooms: {MaxNumberOfRooms}{(MaxNumberOfRooms == int.MaxValue ? " (default)" : "")}, 
                    MinPricePerNight: {MinPricePerNight}{(MinPricePerNight == 0 ? " (default)" : "")}, 
                    MaxPricePerNight: {MaxPricePerNight}{(MaxPricePerNight == int.MaxValue ? " (default)" : "")}, 
                    MinStars: {MinStars}, 
                    MaxStars: {MaxStars}, 
                    City: {City ?? "None"}, 
                    RoomTypes: {(RoomTypes != null ? string.Join(", ", RoomTypes) : "None")}
                    Id: {GetIdStateString()}";
    }

    private string GetIdStateString() =>
        Id.HasValue ? Id.Value.ToString() : "None";

}