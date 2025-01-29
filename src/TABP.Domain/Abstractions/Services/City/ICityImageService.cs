using SixLabors.ImageSharp;

namespace TABP.Domain.Abstractions.Services.City;

public interface ICityImageService
{
    Task AddImagesAsync(Guid cityId, IEnumerable<Image> images);
    Task<IEnumerable<Guid>> GetImageIdsForCityAsync(Guid cityId);
}