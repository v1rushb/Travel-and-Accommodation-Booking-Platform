using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.HotelVisit;

namespace TABP.Domain.Abstractions.Services;

/// <summary>
/// Provides operations for managing hotel visits and retrieving visit statistics.
/// </summary>
public interface IHotelVisitService
{
    /// <summary>
    /// Adds a new hotel visit record.
    /// </summary>
    /// <param name="newHotelVisit">The HotelVisit DTO containing details of the hotel visit.</param>
    Task AddAsync(HotelVisitDTO newHotelVisit);

    /// <summary>
    /// Retrieves the top 5 most visited hotels.
    /// </summary>
    /// <param name="query">The query specifying the time for filtering visits as<see cref="Enums.TimeOptions"/>.</param>
    /// <returns>A collection of the top 5 most visited hotels.</returns>
    /// <exception cref="FluentValidation.ValidationException">
    /// Thrown if the query parameters are invalid.
    /// </exception>
    Task<IEnumerable<VisitedHotelDTO>> GetTop5VisitedHotels(VisitTimeOptionQuery query);
}