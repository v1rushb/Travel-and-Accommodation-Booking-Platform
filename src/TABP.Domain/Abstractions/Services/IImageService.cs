using SixLabors.ImageSharp;
using TABP.Domain.Models.Image;

namespace TABP.Domain.Abstractions.Services;

/// <summary>
/// Provides operations for managing images associated with entities.
/// </summary>
public interface IImageService
{
    /// <summary>
    /// Adds images for a specific entity.
    /// </summary>
    /// <param name="entityId">The unique<see cref="Guid"/>identifier of the entity.</param>
    /// <param name="images">A collection of images to be added.</param>
    /// <exception cref="FluentValidation.ValidationException">Thrown if the images are invalid.</exception>
    Task AddAsync(Guid entityId, IEnumerable<Image> images);

    /// <summary>
    /// Checks if an image exists by its unique<see cref="Guid"/>identifier.
    /// </summary>
    /// <param name="Id">The unique<see cref="Guid"/>identifier of the image.</param>
    /// <returns><c>true</c> if the image exists; otherwise, <c>false</c>.</returns>
    Task<bool> ExistsAsync(Guid Id);

    /// <summary>
    /// Retrieves an image as a stream.
    /// </summary>
    /// <param name="Id">The unique<see cref="Guid"/>identifier of the image.</param>
    /// <param name="imageSize">The desired size of the image.</param>
    /// <returns>A stream containing the image data.</returns>
    /// <exception cref="FluentValidation.ValidationException">Thrown if the provided image size is invalid.</exception>
    /// <exception cref="Exceptions.EntityNotFoundException">Thrown if the image does not exist.</exception>
    Task<Stream> GetAsync(Guid Id, ImageSizeDTO imageSize);

    /// <summary>
    /// Gets the number of images associated with a specific entity.
    /// </summary>
    /// <param name="Id">The unique<see cref="Guid"/>identifier of the entity.</param>
    /// <returns>The total number of images linked to the entity.</returns>
    Task<int> GetCountAsync(Guid Id);

    /// <summary>
    /// Retrieves all image IDs associated with a specific entity.
    /// </summary>
    /// <param name="entityId">The unique<see cref="Guid"/>identifier of the entity.</param>
    /// <returns>A collection of image IDs linked to the entity.</returns>
    Task<IEnumerable<Guid>> GetIdsForEntityAsync(Guid entityId);
}