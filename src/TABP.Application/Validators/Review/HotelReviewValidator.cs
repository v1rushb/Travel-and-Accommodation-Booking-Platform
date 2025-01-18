using System.Data;
using FluentValidation;
using TABP.Application.Services;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Constants.Review;
using TABP.Domain.Models.HotelReview;

namespace TABP.Application.Validators.Review;

public class HotelReviewValidator : AbstractValidator<HotelReviewDTO>
{
    public HotelReviewValidator(
        IUserService userService,
        IHotelService hotelService,
        IHotelReviewService hotelReviewService)
    {
        RuleFor(review => review.Feedback)
            .NotNull()
            .Length(HotelReviewConstants.MinFeedbackLength, HotelReviewConstants.MaxFeedbackLength);

        RuleFor(review => review.UserId)
            .NotNull()
            .MustAsync(async (userId, cancellation) =>
                 await userService.ExistsAsync(userId))
            .WithMessage("{PropertyName} does not exist.");
        
        RuleFor(review => review.HotelId)
            .NotNull()
            .MustAsync(async (hotelId, cancellation) => 
                await hotelService.ExistsAsync(hotelId))
            .WithMessage("{PropertyName} does not exist.");

        RuleFor(review => review)
            .MustAsync(async (review, cancellation) => 
                !await hotelReviewService.ExistsByUserAndHotelAsync(review.UserId, review.HotelId))
            .WithMessage("The user has already submitted a review for this hotel.")
            .WithName("HotelReview");
    }
}