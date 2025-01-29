using TABP.Domain.Models.Booking.Search;
using TABP.Domain.Models.Booking.Search.Response;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services.Booking;

public interface IRoomBookingAdminService
{
    Task<IEnumerable<BookingAdminResponseDTO>> SearchAsync(AdminBookingSearchQuery query, PaginationDTO pagination);
}