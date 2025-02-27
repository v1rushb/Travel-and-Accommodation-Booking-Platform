using FluentValidation;
using TABP.Domain.Abstractions.Services.City;
using TABP.Domain.Constants.Hotel;
using TABP.Domain.Models.Hotel;

namespace TABP.Application.Validators.Hotel;

internal class HotelValidator : AbstractValidator<HotelDTO>
{
    public HotelValidator(ICityService cityService)
    {
        RuleFor(hotel => hotel.Name)
            .NotNull()
            .Length(HotelConstants.MinNameLength,
                    HotelConstants.MaxNameLength);

        RuleFor(hotel => hotel.BriefDescription)
            .NotNull()
            .Length(HotelConstants.MinBriefDescriptionLength, 
                    HotelConstants.MaxBriefDescriptionLength);

        RuleFor(hotel => hotel.DetailedDescription)
            .NotNull()
            .Length(HotelConstants.MinDesctiptionLength, 
                    HotelConstants.MaxDesctiptionLength);
        
        RuleFor(hotel => hotel.StarRating)
            .NotNull()
            .InclusiveBetween(HotelConstants.MinStarRating,
                              HotelConstants.MaxStarRating);

        RuleFor(hotel => hotel.Geolocation)
            .NotNull();
        
        RuleFor(hotel => hotel.CityId)
            .MustAsync(async (cityId, cancellation) => await cityService.ExistsAsync(cityId))
            .WithMessage("{PropertyName} does not exist.");
    }
}