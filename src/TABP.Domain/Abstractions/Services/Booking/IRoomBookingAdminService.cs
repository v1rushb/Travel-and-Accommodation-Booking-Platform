using TABP.Domain.Models.Booking.Search;
using TABP.Domain.Models.Booking.Search.Response;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Domain.Abstractions.Services.Booking;

/// <summary>
/// Defines administrative operations for managing room bookings.
/// This interface specifies methods for searching and retrieving room booking information
/// in an administrative context, supporting filtering, sorting, and pagination.
/// </summary>
public interface IRoomBookingAdminService
{
    /// <summary>
    /// Searches for room bookings based on the specified criteria, tailored for administrative users.
    /// This method allows administrators to search through all room bookings in the system,
    /// applying filters, pagination, and sorting to manage and review booking data effectively.
    /// </summary>
    /// <param name="query">
    /// The booking search query containing filtering options.
    /// Includes criteria such as booking ID, user ID, date range, and booking status to narrow down search results.
    /// </param>
    /// <param name="pagination">
    /// The pagination parameters that control the result set size and page number.
    /// Enables efficient handling of large booking datasets by returning results in manageable pages.
    /// </param>
    /// <param name="sortQuery">
    /// The sorting parameters that define the order of the results.
    /// Allows administrators to sort bookings based on various fields such as booking date, check-in date, etc.
    /// </param>
    /// <returns>
    /// A collection of <see cref="BookingAdminResponseDTO"/> representing room bookings that match the search criteria.
    /// Each <see cref="BookingAdminResponseDTO"/> object contains detailed booking information suitable for administrative review.
    /// </returns>
    Task<IEnumerable<BookingAdminResponseDTO>> SearchAsync(
        AdminBookingSearchQuery query,
        PaginationDTO pagination,
        BookingSortQuery sortQuery);
}