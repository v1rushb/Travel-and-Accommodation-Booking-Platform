using SixLabors.ImageSharp;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Abstractions.Services.City;
using TABP.Domain.Constants.Image;
using TABP.Domain.Exceptions;

namespace TABP.Application.Services.City;

public class CityImageService : ICityImageService
{
    private readonly IImageService _imageService;

    public CityImageService(IImageService imageService)
    {
        _imageService = imageService;
    }
    
    public async Task AddImagesAsync(
        Guid cityId,
        IEnumerable<Image> images)
    {
        await ValidateId(cityId);
        await ValidateNumberOfImagesForCityAsync(cityId, images.Count());

        await _imageService.AddAsync(cityId, images);
    }

    private async Task ValidateNumberOfImagesForCityAsync(
        Guid cityId,
        int numberOfImagesToAdd)
    {
        var numberOfStoredImages = await _imageService
            .GetCountAsync(cityId);

        if(numberOfStoredImages + numberOfImagesToAdd > 
            ImageConstants.MaxNumberOfImages)
        {
            throw new EntityImageLimitExceededException();
        }
    }

    public async Task<IEnumerable<Guid>> GetImageIdsForCityAsync(Guid cityId)
    {
        await ValidateId(cityId);
        
        return await _imageService.GetIdsForEntityAsync(cityId);
    }

    private async Task ValidateId(Guid Id)
    {
        if(!await _imageService.ExistsAsync(Id))
            throw new KeyNotFoundException($"Id {Id} Does not exist.");
    }
}