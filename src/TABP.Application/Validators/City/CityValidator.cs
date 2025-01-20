using FluentValidation;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Constants.City;
using TABP.Domain.Models.City;

namespace TABP.Application.Validators.City;

public class CityValidator : AbstractValidator<CityDTO>
{
    public CityValidator(ICityRepository cityRepository)
    {
        RuleFor(city => city.Name)
            .NotNull()
            .Length(CityConstants.MinNameLength, CityConstants.MaxNameLength);

        RuleFor(city => city.CountryName)
            .NotNull()
            .Length(CityConstants.MinCountryNameLength, CityConstants.MaxCountryNameLength);
        
        RuleFor(city => city)
            .MustAsync(async (city, cancellation) => 
                !await cityRepository.ExistsByNameAndCountryAsync(city.Name, city.CountryName))
                .WithMessage("City with the same name and country already exists.");
    }
}