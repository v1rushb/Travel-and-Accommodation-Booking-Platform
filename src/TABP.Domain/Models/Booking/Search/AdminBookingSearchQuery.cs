namespace TABP.Domain.Models.Booking.Search;

public class AdminBookingSearchQuery
{
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public decimal MinPrice { get; set; } = 0;
    public decimal MaxPrice { get; set; } = decimal.MaxValue;
    public string Notes { get; set; }
    public int RoomNumber { get; set; } = -1;
    public Guid? HotelId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? Id { get; set; } = null;

    public override string ToString() =>
    @$"
            CheckInDate: {CheckInDate?.ToString("yyyy-MM-dd") ?? "None"} - CheckOutDate: {CheckOutDate?.ToString("yyyy-MM-dd") ?? "None"},
            MinPrice: {MinPrice}{(MinPrice == 0 ? " (default)" : "")}, 
            MaxPrice: {MaxPrice}{(MaxPrice == decimal.MaxValue ? " (default)" : "")}, 
            Notes: {Notes ?? "None"}, 
            RoomNumber: {RoomNumber}{(RoomNumber == 1 ? " (default)" : "")}, 
            HotelId: {GetHotelIdState()}, 
            UserId: {UserId?.ToString() ?? "None"}
            Id: {GetIdStateString()}";

    private string GetIdStateString() =>
    Id.HasValue ? Id.Value.ToString() : "None";
    private string GetHotelIdState() =>
        HotelId.HasValue ? HotelId.Value.ToString() : "None";
}