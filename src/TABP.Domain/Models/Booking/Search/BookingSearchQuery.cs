using TABP.Domain.Enums;

namespace TABP.Domain.Models.Booking.Search;

public class BookingSearchQuery
{
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public DateTime? CheckInDateFrom { get; set; }
    public DateTime? CheckInDateTo { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string Notes { get; set; }
    public BookingStatus Status { get; set; }
    public int RoomNumber { get; set; }
    public Guid HotelId { get; set; }
}