using SixLabors.ImageSharp;

namespace TABP.Domain.Abstractions.Services.Hotel;


/// <summary>
/// Defines operations for managing hotel images.
/// </summary>
public interface IHotelImageService
{

    /// <summary>
    /// Adds a collection of<see cref"Image"/>to the specified hotel.
    /// </summary>
    /// <param name="hotelId">
    /// The unique<see cref"Guid"/>identifier of the hotel.
    /// </param>
    /// <param name="images">
    /// The collection of<see cref"Image"/>to add.
    /// </param>
    Task AddImagesAsync(
        Guid hotelId,
        IEnumerable<Image> images);

    /// <summary>
    /// Retrieves the<see cref"Guid"/>identifiers of all<see cref"Image"/>associated with the specified<see cref"Hotel"/>.
    /// </summary>
    /// <param name="hotelId">
    /// The unique<see cref"Guid"/>identifier of the<see cref"Hotel"/>.
    /// </param>
    /// <returns>
    /// A collection of<see cref"Image"/>identifiers.
    /// </returns>
    Task<IEnumerable<Guid>> GetImageIdsForHotelAsync(Guid hotelId);
}