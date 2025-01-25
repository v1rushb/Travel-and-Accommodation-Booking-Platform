using SixLabors.ImageSharp;
using TABP.Domain.Models.Image;

namespace TABP.Domain.Abstractions.Services;

public interface IImageService
{
    Task AddAsync(Guid entityId, IEnumerable<Image> images);
    Task<bool> ExistsAsync(Guid Id);
    Task<Stream> GetAsync(Guid Id, ImageSizeDTO imageSize);
    Task<int> GetCountAsync(Guid Id);
    Task<IEnumerable<Guid>> GetIdsForEntityAsync(Guid entityId);
}