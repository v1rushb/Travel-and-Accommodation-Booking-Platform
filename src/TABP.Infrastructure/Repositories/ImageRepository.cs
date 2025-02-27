using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Models.Image;

namespace TABP.Infrastructure.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly string _mainDirectory =
        Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Images");
    
    private readonly string _imageExtension = "jpeg";
    private readonly ILogger<ImageRepository> _logger;
    

    public ImageRepository(
        HotelBookingDbContext context,
        ILogger<ImageRepository> logger)
    {
        _context = context;
        Directory.CreateDirectory(_mainDirectory);
        _logger = logger;
    }
    public async Task AddAsync(
        Guid entityId,
        IEnumerable<Image> images)
    {
        var imageList = new List<ImageEntity>();

        var logMessage = "";
        foreach(var image in images)
        {
            GenerateAndSaveImage(
                image,
                out var imageId,
                out var imagePath
            );
            var imageEntity = new ImageEntity
            {
                Id = imageId,
                Path = imagePath,
                EntityId = entityId
            };
            imageList.Add(imageEntity);
            logMessage += $"Added Image with Id: {imageId} For Entity with Id: {entityId} With Path: {imagePath}";
        }

        await _context.Images.AddRangeAsync(imageList);
        await _context.SaveChangesAsync();

        _logger.LogInformation(logMessage);
    }
    
    private void GenerateAndSaveImage(Image image,
        out Guid imageId,
        out string imagePath)
    {
        imageId = Guid.NewGuid();
        imagePath = GetFullPath(imageId);
        image.Save(imagePath);
    }
    
    private string GetFullPath(Guid imageId) =>
        Path.Combine(
            _mainDirectory, 
            $"{imageId}.{_imageExtension}");



    public async Task<Stream> GetAsync(
        Guid Id,
        ImageSizeDTO imageSize)
    {
        var imagePath = GetFullPath(Id);

        using var image = (await Image.LoadAsync(imagePath))
            .Clone(image => image.Resize(imageSize.Width, imageSize.Height));

        var memoryStream = new MemoryStream();
        await image.SaveAsJpegAsync(memoryStream);
        memoryStream.Position = 0;

        return memoryStream;
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        ExistsInFileSystem(Id) && await ExistsInDatebaseAsync(Id);
    private bool ExistsInFileSystem(Guid Id) =>
         File.Exists(GetFullPath(Id));

    private Task<bool> ExistsInDatebaseAsync(Guid Id) =>
        _context.Images
            .AnyAsync(image => 
                image.Id == Id);

    public async Task<int> GetCountAsync(Guid Id) =>
        await _context.Images
            .CountAsync(image => image.EntityId == Id);

    public async Task<IEnumerable<Guid>> GetIdsForEntityAsync(Guid entityId) =>
        await _context.Images
            .Where(image => image.EntityId == entityId)
            .Select(image => image.Id)
            .ToListAsync();
}