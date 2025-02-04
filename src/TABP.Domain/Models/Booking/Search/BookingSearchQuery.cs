namespace TABP.Domain.Models.Booking.Search;

public class BookingSearchQuery
{
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public decimal MinPrice { get; set; } = 0;
    public decimal MaxPrice { get; set; } = decimal.MaxValue;
    public string Notes { get; set; }
    public int RoomNumber { get; set; } = 1;
    public Guid HotelId { get; set; }

    public override string ToString() =>
    @$"
            CheckInDate: {CheckInDate?.ToString("yyyy-MM-dd") ?? "None"} - CheckOutDate: {CheckOutDate?.ToString("yyyy-MM-dd") ?? "None"},
            MinPrice: {MinPrice}{(MinPrice == 0 ? " (default)" : "")}, 
            MaxPrice: {MaxPrice}{(MaxPrice == decimal.MaxValue ? " (default)" : "")}, 
            Notes: {Notes ?? "None"}, 
            RoomNumber: {RoomNumber}{(RoomNumber == 1 ? " (default)" : "")},    
            HotelId: {HotelId}";
}