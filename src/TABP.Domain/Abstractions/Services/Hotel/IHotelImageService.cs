using SixLabors.ImageSharp;

namespace TABP.Domain.Abstractions.Services.Hotel;

public interface IHotelImageService
{
    Task AddImagesAsync(
        Guid hotelId,
        IEnumerable<Image> images);
    Task<IEnumerable<Guid>> GetImageIdsForHotelAsync(Guid hotelId);
}