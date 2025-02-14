using SixLabors.ImageSharp;
using TABP.Domain.Models.Image;

namespace TABP.Domain.Abstractions.Repositories;

/// <summary>
/// Defines the contract for a repository to manage images associated with entities.
/// This interface provides asynchronous operations for adding images, checking for existence,
/// retrieving images in different sizes, and counting and fetching image IDs for entities.
/// </summary>
public interface IImageRepository
{
    /// <summary>
    /// Asynchronously adds a collection of images to an entity.
    /// </summary>
    /// <param name="entityId">The unique identifier of the entity to which the images are associated.</param>
    /// <param name="images">An <see cref="IEnumerable{Image}"/> of images to add.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task AddAsync(Guid entityId, IEnumerable<Image> images);

    /// <summary>
    /// Asynchronously checks if an image with the specified ID exists in the repository.
    /// </summary>
    /// <param name="Id">The unique identifier of the image to check.</param>
    /// <returns>A <see cref="Task{bool}"/> representing the asynchronous operation, and upon completion,
    /// returns <c>true</c> if an image with the given ID exists; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> ExistsAsync(Guid Id);

    /// <summary>
    /// Asynchronously retrieves an image stream in a specified size.
    /// </summary>
    /// <param name="Id">The unique identifier of the image.</param>
    /// <param name="imageSize">A <see cref="ImageSizeDTO"/> specifying the desired size of the image.</param>
    /// <returns>A <see cref="Task{Stream}"/> representing the asynchronous operation, and upon completion,
    /// returns a <see cref="Stream"/> for the image in the requested size, or <c>null</c> if the image is not found.
    /// </returns>
    Task<Stream> GetAsync(Guid Id, ImageSizeDTO imageSize);

    /// <summary>
    /// Asynchronously retrieves the count of images associated with a specific entity.
    /// </summary>
    /// <param name="Id">The unique identifier of the entity.</param>
    /// <returns>A <see cref="Task{int}"/> representing the asynchronous operation, and upon completion,
    /// returns the number of images associated with the entity.
    /// </returns>
    Task<int> GetCountAsync(Guid Id);

    /// <summary>
    /// Asynchronously retrieves a collection of image IDs associated with a specific entity.
    /// </summary>
    /// <param name="entityId">The unique identifier of the entity.</param>
    /// <returns>A <see cref="Task{IEnumerable{Guid}}"/> representing the asynchronous operation,
    /// and upon completion, returns a collection of <see cref="Guid"/> 
    /// representing the IDs of images associated with the entity.
    /// </returns>
    Task<IEnumerable<Guid>> GetIdsForEntityAsync(Guid entityId);
}