using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Image;
using SixLabors.ImageSharp;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Exceptions;
using FluentValidation;

namespace TABP.Application.Services;

public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly IValidator<ImageSizeDTO> _imageSizeValidator;
    private readonly IValidator<IEnumerable<Image>> _imageValidator;

    public ImageService(
        IImageRepository imageRepository,
        IValidator<ImageSizeDTO> imageSizeValidator,
        IValidator<IEnumerable<Image>> imageValidator)
    {
        _imageRepository = imageRepository;
        _imageSizeValidator = imageSizeValidator;
        _imageValidator = imageValidator;
    }

    public async Task AddAsync(Guid entityId, IEnumerable<Image> images)
    {
        await _imageValidator.ValidateAndThrowAsync(images);
        
        await _imageRepository.AddAsync(entityId, images);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _imageRepository.ExistsAsync(Id);

    public async Task<Stream> GetAsync(Guid Id, ImageSizeDTO imageSize)
    {
        await _imageSizeValidator.ValidateAndThrowAsync(imageSize);
        await ValidateId(Id);

        return await _imageRepository.GetAsync(Id, imageSize);
    }

    public async Task<int> GetCountAsync(Guid Id) =>
        await _imageRepository.GetCountAsync(Id);

    public async Task<IEnumerable<Guid>> GetIdsForEntityAsync(Guid entityId) =>
        await _imageRepository.GetIdsForEntityAsync(entityId);

    private async Task ValidateId(Guid Id)
    {
        if(!await ExistsAsync(Id))
        {
            throw new EntityNotFoundException($"Image With Id: {Id} Does not exist.");
        }
    }
}