using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Image;
using SixLabors.ImageSharp;
using TABP.Domain.Abstractions.Services.Hotel;
using TABP.Domain.Abstractions.Services.City;
using Microsoft.AspNetCore.Authorization;
using TABP.Domain.Enums;

namespace TABP.API.Controllers;

// [Authorize(Roles = nameof(RoleType.Admin))]
[Authorize]
[ApiController]
[Route("api")]
public class ImageGetController : ControllerBase
{
    private readonly IImageService _imageService;
    private readonly IHotelImageService _hotelImageService;
    private readonly ICityImageService _cityImageService;
    private readonly IRoomImageService _roomImageService;

    public ImageGetController(
        IImageService imageService,
        IHotelImageService hotelImageService,
        ICityImageService cityImageService,
        IRoomImageService roomImageService)
    {
        _imageService = imageService;
        _hotelImageService = hotelImageService;
        _cityImageService = cityImageService;
        _roomImageService = roomImageService;
    }

    [HttpGet("hotel/{hotelId:guid}/images")]
    public async Task<IActionResult> GetHotelImagesIdsAsync(Guid hotelId)
    {
        var imagesIds = await _hotelImageService
            .GetImageIdsForHotelAsync(hotelId);

        return Ok(imagesIds);
    }

    [HttpGet("city/{cityId:guid}/images")]
    public async Task<IActionResult> GetCityImagesIdsAsync(Guid cityId)
    {
        var imagesIds = await _cityImageService
            .GetImageIdsForCityAsync(cityId);

        return Ok(imagesIds);
    }

    [HttpGet("room/{roomId:guid}/images")]
    public async Task<IActionResult> GetRoomImagesIdsAsync(Guid roomId)
    {
        var imagesIds = await _roomImageService
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