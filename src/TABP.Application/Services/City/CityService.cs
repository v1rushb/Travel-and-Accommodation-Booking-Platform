using FluentValidation;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services.City;
using TABP.Domain.Models.City;

namespace TABP.Application.Services.City;


public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;
    private readonly IValidator<CityDTO> _cityValidator;

    public CityService(
       ICityRepository cityRepository,
       IValidator<CityDTO> cityValidator)
    {
        _cityRepository = cityRepository;
        _cityValidator = cityValidator;
    }
    public async Task AddAsync(CityDTO newCity)
    {
        await _cityValidator.ValidateAndThrowAsync(newCity);

        newCity.CreationDate = DateTime.UtcNow;
        newCity.ModificationDate = DateTime.UtcNow;
    
        await _cityRepository.AddAsync(newCity); 
    }

    public async Task DeleteAsync(Guid Id) 
    {
        await ValidateId(Id);

        await _cityRepository.DeleteAsync(Id);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _cityRepository.ExistsAsync(Id);

    public async Task<CityDTO> GetByIdAsync(Guid Id){
        await ValidateId(Id);

        return await _cityRepository.GetByIdAsync(Id);
    }

    public async Task UpdateAsync(CityDTO updatedCity)
    {
        await ValidateId(updatedCity.Id);

        updatedCity.ModificationDate = DateTime.UtcNow;

        await _cityRepository.UpdateAsync(updatedCity);
    }

    public async Task ValidateId(Guid Id)
    {
        if(! await ExistsAsync(Id))
            throw new KeyNotFoundException($"Id {Id} Does not exist.");
    }
}