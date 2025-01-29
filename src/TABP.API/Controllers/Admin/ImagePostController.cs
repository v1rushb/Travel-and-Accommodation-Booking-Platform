using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Abstractions.Services;
using SixLabors.ImageSharp;
using TABP.API.Extensions;
using TABP.Domain.Abstractions.Services.Hotel;
using TABP.Domain.Abstractions.Services.City;
using Microsoft.AspNetCore.Authorization;
using TABP.Domain.Enums;

namespace TABP.API.Controllers.Admin;

[Authorize(Roles = nameof(RoleType.Admin))]
[ApiController]
[Route("api")]
public class ImageController : ControllerBase
{
    private readonly IHotelImageService _hotelImageService;
    private readonly ICityImageService _cityImageService;
    private readonly IRoomImageService _roomImageService;

    public ImageController(
        IHotelImageService hotelImageService,
        ICityImageService cityImageService,
        IRoomImageService roomImageService)
    {
        _hotelImageService = hotelImageService;
        _cityImageService = cityImageService;
        _roomImageService = roomImageService;
    }

    [HttpPost("hotel/{hotelId:guid}/images")]
    public async Task<IActionResult> AddHotelImagesAsync(
        Guid hotelId, List<IFormFile> imagesForm)
    {
        return await CreateEntityImagesAsync(
            hotelId, 
            imagesForm, 
            _hotelImageService.AddImagesAsync
        );
    }

    [HttpPost("city/{cityId}/images")]
    public async Task<IActionResult> AddCityImagesAsync(
        Guid cityId, 
        List<IFormFile> imagesForm)
    {
        return await CreateEntityImagesAsync(
            cityId, 
            imagesForm, 
            _cityImageService.AddImagesAsync
        );
    }

    [HttpPost("room/{roomId}/images")]
    public async Task<IActionResult> AddRoomImagesAsync(
        Guid roomId, 
        List<IFormFile> imagesForm)
    {
        return await CreateEntityImagesAsync(
            roomId, 
            imagesForm, 
            _roomImageService.AddImagesAsync
        );
    }
    private async Task<IActionResult> CreateEntityImagesAsync(
        Guid Id, 
        List<IFormFile> imagesForm,
        Func<Guid, IEnumerable<Image>, Task> addImagesDelegate)
    {
        var images = imagesForm.ToImages();
        await addImagesDelegate(Id, images);

        return Created();
    }
}