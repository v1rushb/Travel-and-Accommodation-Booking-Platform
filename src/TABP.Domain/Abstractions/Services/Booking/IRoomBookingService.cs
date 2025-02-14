using TABP.Domain.Models.Booking;
using TABP.Domain.Models.Cart;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Domain.Abstractions.Services.Booking;

/// <summary>
/// Defines core operations for managing room bookings.
/// This interface specifies methods for creating, retrieving, and validating room bookings,
/// as well as operations for checking room availability and fetching bookings by hotel.
/// </summary>
public interface IRoomBookingService
{
    /// <summary>
    /// Adds a new room booking based on the contents of a shopping cart.
    /// This method takes a cart containing booking details and creates corresponding room bookings in the system.
    /// It is used to convert a user's cart into confirmed room bookings, typically during the checkout process.
    /// </summary>
    /// <param name="Cart">
    /// <see cref="CartDTO"/> representing the shopping cart containing booking items.
    /// Provides all necessary details for creating room bookings, such as room IDs, check-in/check-out dates, and user information.
    /// </param>
    Task AddAsync(CartDTO Cart);

    /// <summary>
    /// Retrieves a room booking by its unique <see cref="Guid"/> identifier.
    /// This method fetches the details of a specific room booking based on its ID,
    /// providing access to all information related to the booking.
    /// </summary>
    /// <param name="Id">
    /// The unique <see cref="Guid"/> identifier of the room booking to retrieve.
    /// Used to locate the specific booking record in the data store.
    /// </param>
    /// <returns>
    /// <see cref="RoomBookingDTO"/> representing the room booking details if found.
    /// Returns null or throws an exception if the booking with the given ID does not exist, depending on implementation.
    /// </returns>
    Task<RoomBookingDTO> GetByIdAsync(Guid Id);

    /// <summary>
    /// Checks if a room booking exists in the system by its unique <see cref="Guid"/> identifier.
    /// This method is used to verify the presence of a room booking record, typically before
    /// performing operations that depend on its existence, such as retrieval, update, or deletion.
    /// </summary>
    /// <param name="Id">
    /// The unique <see cref="Guid"/> identifier of the room booking to check for existence.
    /// Queries the data store to determine if a booking with this ID is present.
    /// </param>
    /// <returns>
    /// <c>true</c> if a room booking with the given ID exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsAsync(Guid Id);

    /// <summary>
    /// Checks if a room is already booked for a specified date range.
    /// This method is used to determine room availability by checking for booking conflicts
    /// within a given start and end date. It helps prevent double-bookings and ensures accurate availability management.
    /// </summary>
    /// <param name="roomId">
    /// The unique <see cref="Guid"/> identifier of the room to check availability for.
    /// Specifies which room's booking status is being queried.
    /// </param>
    /// <param name="StartingDate">
    /// The starting date of the date range to check for bookings.
    /// Defines the beginning of the period for which room availability is being checked.
    /// </param>
    /// <param name="EndingDate">
    /// The ending date of the date range to check for bookings.
    /// Defines the end of the period for which room availability is being checked.
    /// </param>
    /// <returns>
    /// <c>true</c> if the room is booked for any part of the specified date range; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> RoomIsBookedBetween(Guid roomId, DateTime StartingDate, DateTime EndingDate);

    /// <summary>
    /// Retrieves all room bookings associated with hotels, typically for reporting or analytical purposes.
    /// This method is used to fetch booking data aggregated by hotel, which can be useful for hotel managers
    /// to review booking trends, occupancy rates, and other hotel-specific booking metrics.
    /// </summary>
    /// <returns>
    /// A collection of <see cref="HotelBookingDTO"/> representing room bookings, grouped or associated by hotel.
    /// Each <see cref="HotelBookingDTO"/> object contains booking details relevant to a specific hotel, possibly including aggregated data.
    /// </returns>
    Task<IEnumerable<HotelBookingDTO>> GetByHotelAsync();
}