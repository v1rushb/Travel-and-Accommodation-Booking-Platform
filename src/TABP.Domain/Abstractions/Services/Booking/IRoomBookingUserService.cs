using TABP.Domain.Models.Booking.Search;
using TABP.Domain.Models.Booking.Search.Response;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Domain.Abstractions.Services.Booking;

/// <summary>
/// Defines operations for end-user interactions with room bookings.
/// This interface specifies methods for users to search and retrieve their own room bookings,
/// supporting filtering, sorting, and pagination to manage personal booking information.
/// </summary>
public interface IRoomBookingUserService
{
    /// <summary>
    /// Searches for room bookings based on the specified criteria, intended for end-users.
    /// This method allows users to search their own room bookings, applying filters, pagination, and sorting
    /// to efficiently find and manage their booking history. It is tailored for user self-service booking management.
    /// </summary>
    /// <param name="query">
    /// The booking search query containing filtering options provided by the user.
    /// Includes criteria such as booking ID, hotel name, check-in/check-out dates, and booking status to refine search results.
    /// </param>
    /// <param name="pagination">
    /// The pagination parameters that control the size of the result set and the current page number.
    /// Ensures efficient retrieval of booking information, especially when users have numerous past or upcoming bookings.
    /// </param>
    /// <param name="sortQuery">
    /// The sorting parameters that define the order in which search results are presented to the user.
    /// Allows users to sort their bookings based on date, hotel name, or other relevant criteria for easier review and management.
    /// </param>
    /// <returns>
    /// A collection of <see cref="BookingUserResponseDTO"/> representing room bookings that match the user's search criteria.
    /// Each <see cref="BookingUserResponseDTO"/> object contains booking details relevant to the user, such as booking dates, hotel information, and booking status.
    /// </returns>
    Task<IEnumerable<BookingUserResponseDTO>> SearchAsync(
        BookingSearchQuery query,
        PaginationDTO pagination,
        BookingSortQuery sortQuery);
}