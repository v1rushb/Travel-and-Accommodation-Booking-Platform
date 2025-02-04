using TABP.Domain.Models.Booking.Search;
using TABP.Domain.Models.Booking.Search.Response;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Domain.Abstractions.Services.Booking;

public interface IRoomBookingAdminService
{
    Task<IEnumerable<BookingAdminResponseDTO>> SearchAsync(
        AdminBookingSearchQuery query,
        PaginationDTO pagination,
        BookingSortQuery sortQuery);
}