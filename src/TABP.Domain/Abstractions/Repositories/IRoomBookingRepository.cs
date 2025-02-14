using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Booking;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Domain.Abstractions.Repositories;

/// <summary>
/// Defines the contract for a repository to manage <see cref="RoomBooking"/> entities.
/// This interface provides asynchronous operations for creating, retrieving, and checking room booking data,
/// as well as searching for room bookings and fetching all bookings for hotels.
/// </summary>
public interface IRoomBookingRepository
{
    /// <summary>
    /// Asynchronously adds a collection of room bookings to the repository.
    /// </summary>
    /// <param name="bookings">An <see cref="IEnumerable{RoomBookingDTO}"/> of bookings to add.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task AddAsync(IEnumerable<RoomBookingDTO> bookings);

    /// <summary>
    /// Asynchronously retrieves a room booking from the repository by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the room booking to retrieve.</param>
    /// <returns>A <see cref="Task{RoomBookingDTO}"/> representing the asynchronous operation, and upon completion,
    /// returns the <see cref="RoomBookingDTO"/> if found; otherwise, <c>null</c>.
    /// </returns>
    Task<RoomBookingDTO> GetByIdAsync(Guid Id);

    /// <summary>
    /// Asynchronously checks if a room booking with the specified ID exists in the repository.
    /// </summary>
    /// <param name="Id">The unique identifier of the room booking to check.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion,
    /// returns <c>true</c> if a room booking with the given ID exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsAsync(Guid Id);

    /// <summary>
    /// Asynchronously checks if a room is booked between a specified date range.
    /// </summary>
    /// <param name="roomId">The unique identifier of the room.</param>
    /// <param name="StartingDate">The starting date of the date range.</param>
    /// <param name="EndingDate">The ending date of the date range.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion,
    /// returns <c>true</c> if the room is booked within the specified date range; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> RoomIsBookedBetween(
        Guid roomId,
        DateTime StartingDate,
        DateTime EndingDate
    );

    /// <summary>
    /// Searches for room bookings in the repository based on a predicate, with pagination and optional sorting.
    /// </summary>
    /// <param name="predicate">An <see cref="Expression{Func{RoomBooking, bool}}"/> that defines the search conditions for
    /// room bookings.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of room bookings per page.</param>
    /// <param name="orderByDelegate">An optional <see cref="Func{IQueryable{RoomBooking}, IOrderedQueryable{RoomBooking}}"/> 
    /// delegate to specify the sorting order.</param>
    /// <returns>A <see cref="Task{IEnumerable{RoomBookingDTO}}"/> representing the asynchronous operation, and upon completion,
    /// returns a collection of <see cref="RoomBookingDTO"/> that match the search criteria.
    /// </returns>
    Task<IEnumerable<RoomBookingDTO>> SearchAsync(
        Expression<Func<RoomBooking, bool>> predicate,
        int pageNumber,
        int pageSize,
        Func<IQueryable<RoomBooking>, IOrderedQueryable<RoomBooking>> orderByDelegate = null);

    /// <summary>
    /// Asynchronously retrieves all room bookings, grouped or associated by hotels, based on a predicate.
    /// </summary>
    /// <param name="predicate">An <see cref="Expression{Func{RoomBooking, bool}}"/> that defines the filtering conditions for room bookings.</param>
    /// <returns>A <see cref="Task{IEnumerable{HotelBookingDTO}}"/> representing the asynchronous operation, 
    /// and upon completion, returns a collection of <see cref="HotelBookingDTO"/> that match the search criteria 
    /// and are associated with hotels.
    /// </returns>
    Task<IEnumerable<HotelBookingDTO>> GetAllForHotelsAsync(
        Expression<Func<RoomBooking, bool>> predicate
    );
}
}