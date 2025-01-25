using FluentValidation;
using TABP.Domain.Constants.Image;
using TABP.Domain.Models.Image;

namespace TABP.Application.Validators.Image;

internal class ImageSizeValidator : AbstractValidator<ImageSizeDTO>
{
    public ImageSizeValidator()
    {
        RuleFor(imageSize => imageSize.Width)
            .NotNull()
            .InclusiveBetween(ImageConstants.MinWidth, ImageConstants.MaxWidth);

        RuleFor(imageSize => imageSize.Height)
            .NotNull()
            .InclusiveBetween(ImageConstants.MinHeight, ImageConstants.MaxHeight);
    
        RuleFor(imageSize => imageSize)
            .Must(imageSize => imageSize.Width > 0 && imageSize.Height > 0)
            .WithMessage("At least one of the dimensions must be greater than 0.");
    }
}