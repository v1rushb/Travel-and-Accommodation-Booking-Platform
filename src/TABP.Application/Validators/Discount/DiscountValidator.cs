using FluentValidation;
using TAB.Domain.Constants.Discount;
using TABP.Domain.Abstractions.Repositories.Review;
using TABP.Domain.Enums;
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
            .NotNull()
            .InclusiveBetween(DiscountConstants.MinAmountPercent,
                            DiscountConstants.MaxPercent);

        RuleFor(discount => discount)
            .Must(discount => discount.StartingDate < discount.EndingDate)
            .WithMessage("Starting date must be before Ending date.");

        RuleFor(discount => discount.StartingDate)
            .NotNull();
        
        RuleFor(discount => discount.EndingDate)
            .NotNull();

        RuleFor(discount => discount.HotelId)
            .NotNull()
            .MustAsync(async (hotelId, cancellation) => 
                !await hotelReviewRepository.ExistsAsync(hotelId))
            .WithMessage("{PropertyName} does not exist.");

        RuleFor(x => x.roomType)
            .Must(roomType => Enum.IsDefined(typeof(RoomType), roomType))
            .WithMessage("Invalid RoomType. Allowed values: Pending, Approved, Rejected.");



    }
}