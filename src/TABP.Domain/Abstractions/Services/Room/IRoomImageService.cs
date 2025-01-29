using SixLabors.ImageSharp;

namespace TABP.Domain.Abstractions.Services;
public interface IRoomImageService
{
    Task AddImagesAsync(Guid roomId, IEnumerable<Image> images);
    Task<IEnumerable<Guid>> GetImageIdsForRoomAsync(Guid roomId);
}