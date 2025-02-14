using TABP.Domain.Models.Hotel;

namespace TABP.Domain.Abstractions.Services.Hotel;

/// <summary>
/// Defines operations for managing hotel data.
/// </summary>
public interface IHotelService
{
    /// <summary>
    /// Adds a new<see cref="Hotel"/>.
    /// </summary>
    /// <param name="newHotel">
    /// The<see cref="HotelDTO"/>containing details of the<see cref="Hotel"/>to add.
    /// </param>
    Task AddAsync(HotelDTO newHotel);

    /// <summary>
    /// Updates an existing<see cref="Hotel"/>.
    /// </summary>
    /// <param name="updatedHotel">
    /// The<see cref="HotelDTO"/>containing updated<see cref="Hotel"/>details.
    /// </param>
    Task UpdateAsync(HotelDTO updatedHotel);

    /// <summary>
    /// Deletes a<see cref="Hotel"/>by its unique<see cref="Guid"/>identifier.
    /// </summary>
    /// <param name="Id">
    /// The unique<see cref="Guid"/>identifier of the<see cref="Hotel"/>to delete.
    /// </param>
    Task DeleteAsync(Guid Id);

    /// <summary>
    /// Retrieves a<see cref="Hotel"/>by its unique<see cref="Guid"/>identifier.
    /// </summary>
    /// <param name="Id">
    /// The unique<see cref="Guid"/>identifier of the<see cref="Hotel"/>.
    /// </param>
    /// <returns>
    /// <see cref="HotelDTO"/>Representing hotel details.
    /// </returns>
    Task<HotelDTO> GetByIdAsync(Guid Id);

    /// <summary>
    /// Determines whether a<see cref="Hotel"/>exists by its unique<see cref="Guid"/>identifier.
    /// </summary>
    /// <param name="Id">
    /// The unique<see cref="Guid"/>identifier of the<see cref="Hotel"/>.
    /// </param>
    /// <returns>
    /// <c>true</c> if the hotel exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsAsync(Guid Id);

    /// <summary>
    /// Retrieves the next available room number for the specified<see cref="Hotel"/>.
    /// </summary>
    /// <param name="hotelId">
    /// The unique<see cref="Guid"/>identifier of the hotel.
    /// </param>
    /// <returns>
    /// <see cref="int"/>Representing next room number.
    /// </returns>
    Task<int> GetNextRoomNumberAsync(Guid hotelId);
}