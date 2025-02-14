using SixLabors.ImageSharp;

namespace TABP.Domain.Abstractions.Services.City;


/// <summary>
/// Defines operations for managing city images.
/// This interface provides methods to handle the lifecycle of images associated with cities,
/// including adding new images and retrieving existing image identifiers.
/// </summary>
public interface ICityImageService
{
    /// <summary>
    /// Adds a collection of <see cref="Image"/> to the specified city.
    /// This method is used to associate one or more images with a city,
    /// enhancing its visual representation and user engagement. It handles the storage
    /// and linking of image data to the corresponding city record.
    /// </summary>
    /// <param name="cityId">
    /// The unique <see cref="Guid"/> identifier of the city to which the images are being added.
    /// This ID is used to correctly associate the images with the intended city in the system.
    /// </param>
    /// <param name="images">
    /// A collection of <see cref="Image"/> objects to be added to the city.
    /// This collection may contain multiple images for a city, allowing for a gallery or richer visual context.
    /// </param>
    Task AddImagesAsync(
        Guid cityId,
        IEnumerable<Image> images
    );

    /// <summary>
    /// Retrieves the <see cref="Guid"/> identifiers of all <see cref="Image"/> associated with a specific city.
    /// This method is used to fetch a list of image IDs that are linked to a given city.
    /// These IDs can then be used to retrieve the actual image data for display or further processing.
    /// </summary>
    /// <param name="cityId">
    /// The unique <see cref="Guid"/> identifier of the city for which image IDs are being requested.
    /// The method uses this ID to query the system and find all associated image records.
    /// </param>
    /// <returns>
    /// A collection of <see cref="Guid"/>, each representing the identifier of an image associated with the city.
    /// Returns an empty collection if no images are associated with the city or if the city does not exist.
    /// </returns>
    Task<IEnumerable<Guid>> GetImageIdsForCityAsync(Guid cityId);
}