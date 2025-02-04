using TABP.Domain.Models.Booking.Search;
using TABP.Domain.Models.Booking.Search.Response;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Domain.Abstractions.Services.Booking;

public interface IRoomBookingUserService
{
    Task<IEnumerable<BookingUserResponseDTO>> SearchAsync(
        BookingSearchQuery query,
        PaginationDTO pagination,
        BookingSortQuery sortQuery);
}