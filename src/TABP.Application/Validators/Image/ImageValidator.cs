using FluentValidation;
using SixLabors.ImageSharp;
using TABP.Domain.Constants.Image;

namespace TABP.Application.Vaildator.Images;

internal class ImageValidator : AbstractValidator<IEnumerable<Image>>
{
    public ImageValidator()
    {
        RuleFor(images => images)
            .Must(images => images.Count() >= ImageConstants.MinNumberOfImagesPerRequest)
            .WithMessage("At least one image is required.")

            .Must(images => 
                images.Count() <= ImageConstants.MaxNumberOfImagesPerRequest)
            .WithMessage("Maximum number of images per request is {MaxNumberOfImagesPerRequest}.")

            .Must(images => 
                images.All(image => 
                    IsValidImageHeight(image.Height)))
            .WithMessage("All images must have a height between {MinHeight} and {MaxHeight}.")

            .Must(images => images
                .All(image => 
                    IsValidImageWidth(image.Width)))
            .WithMessage("All images must have a width between {MinWidth} and {MaxWidth}.");
    }
    
    private bool IsValidImageWidth(int width) =>
        width >= ImageConstants.MinWidth &&
             width <= ImageConstants.MaxWidth;

    private bool IsValidImageHeight(int height) =>
        height >= ImageConstants.MinHeight &&
             height <= ImageConstants.MaxHeight;
}