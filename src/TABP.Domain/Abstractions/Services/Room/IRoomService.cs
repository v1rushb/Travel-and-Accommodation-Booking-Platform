using TABP.Domain.Models.Room;

namespace TABP.Domain.Abstractions.Services;


/// <summary>
/// Provides main operations for managing Rooms.
/// </summary>
public interface IRoomService
{
    /// <summary>
    /// Adds a new room.
    /// </summary>
    /// <param name="newRoom">The <see cref="RoomDTO"/>containing details of the room to add.</param>
    /// <exception cref="FluentValidation.ValidationException">
    /// Thrown if the room data is invalid.
    /// </exception>
    Task AddAsync(RoomDTO newRoom);

    /// <summary>
    /// Deletes a room by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique<see cref="Guid"/>identifier of the room to delete.</param>
    /// <exception cref="Exceptions.EntityNotFoundException">
    /// Thrown if the room does not exist.
    /// </exception>
    Task DeleteAsync(Guid Id);

    /// <summary>
    /// Updates an existing room.
    /// </summary>
    /// <param name="updatedRoom">The<see cref="RoomDTO"/>containing updated room details.</param>
    /// <exception cref="FluentValidation.ValidationException">
    /// Thrown if the updated room data is invalid.
    /// </exception>
    /// <exception cref="Exceptions.EntityNotFoundException">
    /// Thrown if the room does not exist.
    /// </exception>
    Task UpdateAsync(RoomDTO updatedRoom);

    /// <summary>
    /// Checks if a room exists by its unique<see cref="Guid"/>identifier.
    /// </summary>
    /// <param name="Id">The unique<see cref="Guid"/>identifier of the room.</param>
    /// <returns>
    /// <c>true</c> if the room exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsAsync(Guid Id);


    /// <summary>
    /// Retrieves a room by its unique<see cref="Guid"/>identifier.
    /// </summary>
    /// <param name="Id">The unique<see cref="Guid"/>identifier of the room.</param>
    /// <returns>
    /// <see cref="RoomDTO"/>representing Room.
    /// </returns>
    /// <exception cref="Exceptions.EntityNotFoundException">
    /// Thrown if the room does not exist.
    /// </exception>
    Task<RoomDTO> GetByIdAsync(Guid Id);

    /// <summary>
    /// Checks if a room number exists for a specific hotel.
    /// </summary>
    /// <param name="hotelId">The unique<see cref="Guid"/>identifier of the hotel.</param>
    /// <param name="RoomId">The unique<see cref="Guid"/>identifier of the room.</param>
    /// <returns>
    /// <c>true</c> if the room number exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> RoomNumberExistsForHotelAsync(
        Guid hotelId,
        Guid RoomId);

    /// <summary>
    /// Calculates the booking price for a room based on the check-in and check-out dates.
    /// </summary>
    /// <param name="RoomId">The unique<see cref="Guid"/>identifier of the room.</param>
    /// <param name="checkInDate">The check-in date.</param>
    /// <param name="checkOutDate">The check-out date.</param>
    /// <returns>
    /// Booking price as a <see cref="decimal"/>.
    /// </returns>
    Task<decimal> GetBookingPriceForRoom(
        Guid RoomId,
        DateTime checkInDate,
        DateTime checkOutDate);
}