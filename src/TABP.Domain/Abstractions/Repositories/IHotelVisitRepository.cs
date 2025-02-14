using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.HotelVisit;

namespace TABP.Domain.Abstractions.Repositories;

/// <summary>
/// Defines the contract for a repository to manage <see cref="HotelVisit"/> entities.
/// This interface provides asynchronous operations for creating hotel visit data,
/// as well as checking for existence and retrieving visited hotels based on various criteria.
/// </summary>
public interface IHotelVisitRepository 
{
    /// <summary>
    /// Asynchronously adds a new hotel visit record to the repository.
    /// </summary>
    /// <param name="newHotelVisit">A <see cref="HotelVisitDTO"/> containing the data for the new hotel visit.</param>
    /// <returns>A <see cref="Task{Guid}"/> representing the asynchronous operation, and upon completion,
    /// returns the unique identifier of the newly added hotel visit record.
    /// </returns>
    Task<Guid> AddAsync(HotelVisitDTO newHotelVisit);

    /// <summary>
    /// Asynchronously checks if a hotel visit with the specified ID exists in the repository.
    /// </summary>
    /// <param name="Id">The unique identifier of the hotel visit to check.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion,
    /// returns <c>true</c> if a hotel visit with the given ID exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsAsync(Guid Id);

    /// <summary>
    /// Asynchronously retrieves visited hotels based on a predicate, with pagination.
    /// </summary>
    /// <param name="predicate">An <see cref="Expression{Func{HotelVisit, bool}}"/> that defines the criteria 
    /// for filtering visited hotels.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of visited hotels per page.</param>
    /// <returns>A <see cref="Task{IEnumerable{VisitedHotelDTO}}"/> representing the asynchronous operation,
    /// and upon completion, returns a collection of <see cref="VisitedHotelDTO"/> that match the search criteria, paginated.
    /// </returns>
    Task<IEnumerable<VisitedHotelDTO>> GetVisitedHotels(
        Expression<Func<HotelVisit, bool>> predicate,
        int pageNumber,
        int pageSize);
}