using FluentValidation;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotels;

namespace TABP.Application.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IValidator<HotelDTO> _hotelValidator;

    public HotelService(
        IHotelRepository hotelRepository,
        IValidator<HotelDTO> hotelValidator)
    {
        _hotelRepository = hotelRepository;
        _hotelValidator = hotelValidator;
    }
    public async Task<Guid> AddAsync(HotelDTO newHotel)
    {
        await _hotelValidator.ValidateAndThrowAsync(newHotel);

        newHotel.CreationDate = DateTime.UtcNow;
        newHotel.ModificationDate = DateTime.UtcNow;

        return await _hotelRepository.AddAsync(newHotel);
    }

    public async Task DeleteAsync(Guid Id)
    {
        await ValidateId(Id);

        await _hotelRepository.DeleteAsync(Id);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _hotelRepository.ExistsAsync(Id);

    public async Task<Hotel> GetByIdAsync(Guid Id) {
        await ValidateId(Id);
        return await _hotelRepository.GetByIdAsync(Id);
    }

    public async Task UpdateAsync(HotelDTO updatedHotel)
    {
        await _hotelValidator.ValidateAndThrowAsync(updatedHotel);
        
        updatedHotel.ModificationDate = DateTime.UtcNow;
        await _hotelRepository.UpdateAsync(updatedHotel);
    }

    private async Task ValidateId(Guid Id)
    {
        if(!await ExistsAsync(Id))
            throw new KeyNotFoundException($"Id {Id} Does not exist.");
    }
}