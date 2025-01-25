using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Abstractions.Services;
using SixLabors.ImageSharp;
using TABP.API.Extensions;

namespace TABP.API.Controllers;

[ApiController]
[Route("api")]
public class ImageController : ControllerBase
{
    private readonly IHotelService _hotelService;
    private readonly ICityService _cityService;
    private readonly IRoomService _roomService;

    public ImageController(
        IHotelService hotelService,
        ICityService cityService,
        IRoomService roomService)
    {
        _hotelService = hotelService;
        _cityService = cityService;
        _roomService = roomService;
    }

    [HttpPost("hotel/{hotelId:guid}/images")]
    public async Task<IActionResult> AddHotelImagesAsync(
        Guid hotelId, List<IFormFile> imagesForm)
    {
        System.Console.WriteLine(imagesForm.Count);
        return await CreateEntityImagesAsync(
            hotelId, 
            imagesForm, 
            _hotelService.AddImagesAsync
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
            _cityService.AddImagesAsync
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
            _roomService.AddImagesAsync
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