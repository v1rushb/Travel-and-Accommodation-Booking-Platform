using SixLabors.ImageSharp;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Abstractions.Services.Hotel;
using TABP.Domain.Constants.Image;
using TABP.Domain.Exceptions;

namespace TABP.Application.Services.Hotel;

public class HotelImageService : IHotelImageService
{
    private readonly IImageService _imageService;

    public HotelImageService(IImageService imageService)
    {
        _imageService = imageService;
    }

    public async Task AddImagesAsync(
        Guid hotelId,
        IEnumerable<Image> images)
    {
        await ValidateId(hotelId);
        await ValidateNumberOfImagesForHotelAsync(
            hotelId,
            images.Count()
        );

        await _imageService.AddAsync(hotelId, images);
    }

    private async Task ValidateNumberOfImagesForHotelAsync(
        Guid hotelId,
        int numberOfImagesToAdd)
    {
        var numberOfStoredImages = await _imageService
            .GetCountAsync(hotelId);

        if (numberOfStoredImages + numberOfImagesToAdd >
            ImageConstants.MaxNumberOfImages)
        {
            throw new EntityImageLimitExceededException();
        }
    }

    public async Task<IEnumerable<Guid>> GetImageIdsForHotelAsync(Guid hotelId)
    {
        await ValidateId(hotelId);
        return await _imageService.GetIdsForEntityAsync(hotelId);
    }

    private async Task ValidateId(Guid Id)
    {
        if (!await _imageService.ExistsAsync(Id))
            throw new KeyNotFoundException($"Id {Id} Does not exist.");
    }
}