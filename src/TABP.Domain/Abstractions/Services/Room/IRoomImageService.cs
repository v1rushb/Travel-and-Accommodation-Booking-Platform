using SixLabors.ImageSharp;

namespace TABP.Domain.Abstractions.Services;

/// <summary>
/// Defines methods for performing administrative operations regarding images.
/// </summary>
public interface IRoomImageService
{
    /// <summary>
    /// Adds a collection of images to a room.
    /// </summary>
    /// <param name="roomId">The unique<see cref="Guid"/>identifier of the room.</param>
    /// <param name="images">The collection of images to add.</param>
    /// <exception cref="Exceptions.EntityNotFoundException">
    /// Thrown if the room does not exist.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if adding the images would exceed the maximum allowed images per room.
    /// </exception>
    Task AddImagesAsync(
        Guid roomId,
        IEnumerable<Image> images
    );

    /// <summary>
    /// Retrieves the identifiers of all images associated with a specific Room.
    /// </summary>
    /// <param name="roomId">The unique<see cref="Guid"/>identifier of the room.</param>
    /// <returns>
    /// A collection of image identifiers<see cref="Guid"/>.
    /// </returns>
    /// <exception cref="Exceptions.EntityNotFoundException">
    /// Thrown if the room does not exist.
    /// </exception>
    Task<IEnumerable<Guid>> GetImageIdsForRoomAsync(Guid roomId);
}