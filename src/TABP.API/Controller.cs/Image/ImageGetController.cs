using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Image;
using SixLabors.ImageSharp;

namespace TABP.API.Controllers;

[ApiController]
[Route("api")]
public class ImageGetController : ControllerBase
{
    private readonly IImageService _imageService;
    private readonly IHotelService _hotelService;
    private readonly ICityService _cityService;
    private readonly IRoomService _roomService;

    public ImageGetController(
        IImageService imageService,
        IHotelService hotelService,
        ICityService cityService,
        IRoomService roomService)
    {
        _imageService = imageService;
        _hotelService = hotelService;
        _cityService = cityService;
        _roomService = roomService;
    }

    // private async Task<IActionResult> GetEntityImagesAsync()
    [HttpGet("hotel/{hotelId:guid}/images")]
    public async Task<IActionResult> GetHotelImagesIdsAsync(Guid hotelId)
    {
        var imagesIds = await _hotelService
            .GetImageIdsForHotelAsync(hotelId);

        return Ok(imagesIds);
    }

    [HttpGet("city/{cityId:guid}/images")]
    public async Task<IActionResult> GetCityImagesIdsAsync(Guid cityId)
    {
        var imagesIds = await _cityService
            .GetImageIdsForCityAsync(cityId);

        return Ok(imagesIds);
    }

    [HttpGet("room/{roomId:guid}/images")]
    public async Task<IActionResult> GetRoomImagesIdsAsync(Guid roomId)
    {
        var imagesIds = await _roomService
            .GetImageIdsForRoomAsync(roomId);

        return Ok(imagesIds);
    }

    [HttpGet("hotels/images/{imageId:guid}")]
    public async Task<IActionResult> GetHotelImageAsync(
        Guid imageId,
        [FromQuery] ImageSizeDTO imageSize) =>
    await GetEntityImagesAsync(
        imageId,
        imageSize,
        _imageService.GetAsync
    );

    [HttpGet("cities/images/{imageId:guid}")]
    public async Task<IActionResult> GetCityImageAsync(
        Guid imageId,
        [FromQuery] ImageSizeDTO imageSize) =>
    await GetEntityImagesAsync(
        imageId,
        imageSize,
        _imageService.GetAsync
    );

    [HttpGet("rooms/images/{imageId:guid}")]
    public async Task<IActionResult> GetRoomImageAsync(
        Guid imageId,
        [FromQuery] ImageSizeDTO imageSize) =>
    await GetEntityImagesAsync(
        imageId,
        imageSize,
        _imageService.GetAsync
    );


    private async Task<IActionResult> GetEntityImagesAsync(
        Guid imageId,
        ImageSizeDTO imageSize,
        Func<Guid, ImageSizeDTO, Task<Stream>> getImageDelegate)
    {
        var imageStream = await getImageDelegate(imageId, imageSize);
        var imageFormat = await Image.DetectFormatAsync(imageStream);
        return File(imageStream, imageFormat.DefaultMimeType);
    }
}