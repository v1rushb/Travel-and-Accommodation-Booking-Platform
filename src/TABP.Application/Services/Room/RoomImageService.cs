using SixLabors.ImageSharp;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Constants.Image;
using TABP.Domain.Exceptions;

namespace TABP.Application.Services.Room;

public class RoomImageService : IRoomImageService
{
    private readonly IImageService _imageService;

    public RoomImageService(IImageService imageService)
    {
        _imageService = imageService;
    }

    public async Task AddImagesAsync(
        Guid roomId,
        IEnumerable<Image> images)
    {
        await ValidateId(roomId);
        await ValidateNumberOfImagesForRoomAsync(
            roomId,
            images.Count()
        );

        await _imageService.AddAsync(roomId, images);
    }

    private async Task ValidateNumberOfImagesForRoomAsync(
        Guid roomId,
        int numberOfImagesToAdd)
    {
        var numberOfStoredImages = await _imageService
            .GetCountAsync(roomId);

        if (numberOfStoredImages + numberOfImagesToAdd >
            ImageConstants.MaxNumberOfImages)
        {
            throw new InvalidOperationException(
                $"The number of images for room {roomId} exceeds the maximum number of images per room.");
        }
    }

    public async Task<IEnumerable<Guid>> GetImageIdsForRoomAsync(Guid hotelId)
    {
        await ValidateId(hotelId);

        return await _imageService
            .GetIdsForEntityAsync(hotelId);
    }

    private async Task ValidateId(Guid Id)
    {
        if (!await _imageService.ExistsAsync(Id))
            throw new EntityNotFoundException($"Id {Id} Does not exist.");
    }
}