using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
namespace TABP.API.Extensions;

public static class ImageExtensions
{
    public static IEnumerable<Image<Rgba32>> ToImages(this List<IFormFile> imagesForm) =>
        imagesForm.Select(form => form.ToImage());

    private static Image<Rgba32> ToImage(this IFormFile form)
    {
        using var stream = form.OpenReadStream();
        return Image.Load<Rgba32>(stream);
    }
}