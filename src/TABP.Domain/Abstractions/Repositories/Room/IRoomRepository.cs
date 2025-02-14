using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Room;

namespace TABP.Domain.Abstractions.Repositories;

/// <summary>
/// Defines the contract for a repository to manage <see cref="Room"/> entities.
/// This interface provides asynchronous operations for creating, retrieving, updating, and deleting room data,
/// as well as searching for rooms and checking for their existence within a hotel.
/// </summary>
public interface IRoomRepository
{
    /// <summary>
    /// Asynchronously adds a new room to the repository.
    /// </summary>
    /// <param name="newRoom">A <see cref="RoomDTO"/> containing the data for the new room.</param>
    /// <returns>A <see cref="Task{Guid}"/> representing the asynchronous operation, and upon completion,
    /// returns the unique identifier of the newly added room.
    /// </returns>
    Task<Guid> AddAsync(RoomDTO newRoom);


    /// <summary>
    /// Asynchronously checks if a room with the specified ID exists in the repository.
    /// </summary>
    /// <param name="Id">The unique identifier of the room to check.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion,
    /// returns <c>true</c> if a room with the given ID exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsAsync(Guid Id);


    /// <summary>
    /// Asynchronously deletes a room from the repository by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the room to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteAsync(Guid Id);

    /// <summary>
    /// Asynchronously updates an existing room in the repository.
    /// </summary>
    /// <param name="updatedRoom">A <see cref="RoomDTO"/> containing the updated data for the room.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateAsync(RoomDTO updatedRoom);


    /// <summary>
    /// Asynchronously retrieves a room from the repository by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the room to retrieve.</param>
    /// <returns>A <see cref="Task{RoomDTO}"/> representing the asynchronous operation, and upon completion,
    /// returns the <see cref="RoomDTO"/>
    /// </returns>
    Task<RoomDTO> GetByIdAsync(Guid Id);

    /// <summary>
    /// Asynchronously retrieves all rooms associated with a specific hotel.
    /// </summary>
    /// <param name="HotelId">The unique identifier of the hotel.</param>
    /// <returns>A <see cref="Task{IEnumerable{Room}}"/> representing the asynchronous operation, and upon completion,
    /// returns a collection of <see cref="Room"/> entities associated with the specified hotel.
    /// </returns>
    Task<IEnumerable<Room>> GetRoomsByHotelAsync(Guid HotelId);

    /// <summary>
    /// Asynchronously checks if a room exists for a given hotel.
    /// </summary>
    /// <param name="HotelId">The unique identifier of the hotel.</param>
    /// <param name="RoomId">The unique identifier of the room.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion,
    /// returns <c>true</c> if a room with the given ID exists within the specified hotel; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> RoomExistsForHotelAsync(Guid HotelId, Guid RoomId);

    /// <summary>
    /// Searches for rooms in the repository based on availability criteria, with pagination and optional sorting.
    /// </summary>
    /// <param name="predicate">An <see cref="Expression{Func{RoomWithAvailability, bool}}"/> that defines the search conditions, including availability.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of rooms per page.</param>
    /// <param name="orderBy">An optional <see cref="Func{IQueryable{RoomWithAvailability}, IOrderedQueryable{RoomWithAvailability}}"/> delegate to specify the sorting order for results of type <see cref="RoomWithAvailability"/>.</param>
    /// <returns>A <see cref="Task{IEnumerable{RoomDTO}}"/> representing the asynchronous operation, and upon completion,
    /// returns a collection of <see cref="RoomDTO"/> that match the search criteria, including availability information.
    /// </returns>
    Task<IEnumerable<RoomDTO>> SearchAsync(
        Expression<Func<RoomWithAvailability, bool>> predicate,
        int pageNumber,
        int pageSize,
        Func<IQueryable<RoomWithAvailability>, IOrderedQueryable<RoomWithAvailability>> orderBy = null);
}