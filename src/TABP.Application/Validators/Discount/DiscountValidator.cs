using FluentValidation;
using TAB.Domain.Constants.Discount;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Models.Discount;

namespace TABP.Application.Validators.Discount;

internal class DiscountValidator : AbstractValidator<DiscountDTO>
{
    public DiscountValidator(IHotelReviewRepository hotelReviewRepository)
    {
        RuleFor(discount => discount.Reason)
            .NotNull()
            .Length(DiscountConstants.MinReasonLength,
                    DiscountConstants.MaxReasonLength);
        
        RuleFor(discount => discount.AmountPercentage)
            .InclusiveBetween(DiscountConstants.MinAmountPercent,
                            DiscountConstants.MaxPercent);

        RuleFor(discount => discount)
            .Must(discount => discount.StartingDate < discount.EndingDate)
            .WithMessage("Starting date must be before Ending date.");

        RuleFor(discount => discount.HotelId)
            .NotNull()
            .MustAsync(async (hotelId, cancellation) => await hotelReviewRepository.ExistsAsync(hotelId))
            .WithMessage("{PropertyName} does not exist.");

    }
}