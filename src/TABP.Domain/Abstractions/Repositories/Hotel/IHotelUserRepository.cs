using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Hotels;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.Hotel.Search.Response;

namespace TABP.Domain.Abstractions.Repositories;

/// <summary>
/// Defines the contract for a repository focused on user-specific hotel interactions and data retrieval.
/// This interface provides asynchronous operations for fetching hotel page details, weekly featured hotels,
/// hotel history for users, and hotel deals, tailored for user-centric functionalities.
/// </summary>
public interface IHotelUserRepository
{
    /// <summary>
    /// Asynchronously retrieves detailed information for a specific hotel's page, including related data for user display.
    /// </summary>
    /// <param name="hotelId">The unique identifier of the hotel for which to retrieve the page.</param>
    /// <returns>A <see cref="Task{HotelPageResponseDTO}"/> representing the asynchronous operation, and upon completion,
    /// returns a <see cref="HotelPageResponseDTO"/> containing hotel page details
    /// </returns>
    Task<HotelPageResponseDTO> GetHotelPageAsync(Guid hotelId);


    /// <summary>
    /// Asynchronously retrieves a collection of weekly featured hotels based on a given predicate.
    /// </summary>
    /// <param name="predicate">An <see cref="Expression{Func{Hotel, bool}}"/> that defines the criteria for featured hotels.</param>
    /// <returns>A <see cref="Task{IEnumerable{VisitedHotelDTO}}"/> representing the asynchronous operation, and upon completion,
    /// returns a collection of <see cref="VisitedHotelDTO"/> representing weekly featured hotels.
    /// </returns>
    Task<IEnumerable<VisitedHotelDTO>> GetWeeklyFeaturedHotelsAsync(
        Expression<Func<Hotel, bool>> predicate);

    /// <summary>
    /// Asynchronously retrieves the history of hotels visited by a user, based on a predicate and with pagination.
    /// </summary>
    /// <param name="predicate">An <see cref="Expression{Func{HotelVisit, bool}}"/> that defines the criteria for hotel visits to filter the history.</param>
    /// <param name="userId">The unique identifier of the user for whom to retrieve the hotel history.</param>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of history items per page.</param>
    /// <returns>A <see cref="Task{IEnumerable{HotelHistoryDTO}}"/> representing the asynchronous operation, and upon completion,
    /// returns a collection of <see cref="HotelHistoryDTO"/> representing the user's hotel visit history, paginated.
    /// </returns>
    Task<IEnumerable<HotelHistoryDTO>> GetHotelHistoryAsync(
        Expression<Func<HotelVisit, bool>> predicate,
        Guid userId,
        int pageNumber,
        int pageSize);

    /// <summary>
    /// Asynchronously retrieves a collection of hotel deals currently available.
    /// </summary>
    /// <returns>A <see cref="Task{IEnumerable{HotelDealDTO}}"/> representing the asynchronous operation, and upon completion,
    /// returns a collection of <see cref="HotelDealDTO"/> containing information about available hotel deals.
    /// </returns>
    Task<IEnumerable<HotelDealDTO>> GetDealsAsync();

}