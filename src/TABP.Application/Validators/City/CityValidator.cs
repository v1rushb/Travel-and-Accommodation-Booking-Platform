using FluentValidation;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Constants.City;
using TABP.Domain.Models.City;

namespace TABP.Application.Validators.City;

public class CityValidator : AbstractValidator<CityDTO>
{
    public CityValidator()
    {
        RuleFor(city => city.Name)
            .NotNull()
            .Length(CityConstants.MinNameLength, CityConstants.MaxNameLength);

        RuleFor(city => city.CountryName)
            .NotNull()
            .Length(CityConstants.MinCountryNameLength, CityConstants.MaxCountryNameLength);
    }
}