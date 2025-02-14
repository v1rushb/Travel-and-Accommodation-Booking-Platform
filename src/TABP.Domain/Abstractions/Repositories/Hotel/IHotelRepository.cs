using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.Hotels;

namespace TABP.Domain.Abstractions.Repositories;

/// <summary>
/// Defines the contract for a repository to manage <see cref="Hotel"/> entities.
/// This interface provides asynchronous operations for creating, retrieving, updating, and deleting hotel data,
/// as well as searching, fetching the next room number, and updating hotel information.
/// </summary>
public interface IHotelRepository
{
    /// <summary>
    /// Asynchronously adds a new hotel to the repository.
    /// </summary>
    /// <param name="newHotel">A <see cref="HotelDTO"/> containing the data for the new hotel.</param>
    /// <returns>A <see cref="Task{Guid}"/> representing the asynchronous operation, and upon completion,
    /// returns the unique identifier of the newly added hotel.
    /// </returns>
    Task<Guid> AddAsync(HotelDTO newHotel);

    /// <summary>
    /// Asynchronously checks if a hotel with the specified ID exists in the repository.
    /// </summary>
    /// <param name="Id">The unique identifier of the hotel to check.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion,
    /// returns <c>true</c> if a hotel with the given ID exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsAsync(Guid Id);

    /// <summary>
    /// Asynchronously deletes a hotel from the repository by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the hotel to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteAsync(Guid Id);

    /// <summary>
    /// Asynchronously updates an existing hotel in the repository.
    /// </summary>
    /// <param name="updatedHotel">A <see cref="HotelDTO"/> containing the updated data for the hotel.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateAsync(HotelDTO updatedHotel);

    /// <summary>
    /// Asynchronously retrieves a hotel from the repository by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the hotel to retrieve.</param>
    /// <returns>A <see cref="Task{HotelDTO}"/> representing the asynchronous operation, and upon completion,
    /// returns the <see cref="HotelDTO"/>
    /// </returns>
    Task<HotelDTO> GetByIdAsync(Guid Id);


    /// <summary>
    /// Asynchronously retrieves the next available room number for a specific hotel.
    /// </summary>
    /// <param name="hotelId">The unique identifier of the hotel.</param>
    /// <returns>A <see cref="Task{int}"/> representing the asynchronous operation, and upon completion, returns the next available room number for the hotel.</returns>
    Task<int> GetNextRoomNumberAsync(Guid hotelId);

    /// <summary>
    /// Searches for hotels in the repository based on a predicate, with pagination, revenue predicate, and optional sorting.
    /// </summary>
    /// <param name="predicate">An <see cref="Expression{Func{Hotel, bool}}"/> that defines the search conditions for hotels.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of hotels per page.</param>
    /// <param name="revenuePredicate">An <see cref="Expression{Func{RoomBooking, bool}}"/> that defines conditions for revenue calculation.</param>
    /// <param name="orderBy">An optional <see cref="Func{IQueryable{HotelInsightDTO},IOrderedQueryable{HotelInsightDTO}}"/> delegate to specify the sorting order for results of type <see cref="HotelInsightDTO"/>.</param>
    /// <returns>A <see cref="Task{IEnumerable{HotelInsightDTO}}"/> representing the asynchronous operation, and upon completion,
    /// returns a collection of <see cref="HotelInsightDTO"/> that match the search criteria, including hotel insights.
    /// </returns>
    Task<IEnumerable<HotelInsightDTO>> SearchAsync(
        Expression<Func<Hotel, bool>> predicate,
        int pageNumber,
        int pageSize,
        Expression<Func<RoomBooking, bool>> revenuePredicate,
        Func<IQueryable<HotelInsightDTO>,IOrderedQueryable<HotelInsightDTO>> orderBy 
            = null);

    /// <summary>
    /// Asynchronously retrieves the name of a hotel by its unique identifier.
    /// </summary>
    /// <param name="Id">The unique identifier of the hotel.</param>
    /// <returns>A <see cref="Task{string}"/> representing the asynchronous operation,
    /// and upon completion, returns the name of the hotel if found; otherwise, <c>null</c>.</returns>
    Task<string> GetHotelNameByIdAsync(Guid Id);

    /// <summary>
    /// Updates an existing hotel in the repository. This is a void return synchronous operation.
    /// </summary>
    /// <param name="Hotel">A <see cref="HotelDTO"/> containing the updated data for the hotel.</param>
    void Update(HotelDTO Hotel);
}