using FluentValidation;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services.Hotel;
using TABP.Domain.Exceptions;
using TABP.Domain.Models.Hotel;

namespace TABP.Application.Services.Hotel;

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
    public async Task AddAsync(HotelDTO newHotel)
    {
        await _hotelValidator.ValidateAndThrowAsync(newHotel);

        newHotel.CreationDate = DateTime.UtcNow;
        newHotel.ModificationDate = DateTime.UtcNow;

        await _hotelRepository.AddAsync(newHotel);
    }

    public async Task DeleteAsync(Guid Id)
    {
        await ValidateId(Id);

        await _hotelRepository.DeleteAsync(Id);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _hotelRepository.ExistsAsync(Id);

    public async Task<HotelDTO> GetByIdAsync(Guid Id)
    {
        await ValidateId(Id);
        return await _hotelRepository.GetByIdAsync(Id);
    }

    public async Task UpdateAsync(HotelDTO updatedHotel)
    {
        await _hotelValidator.ValidateAndThrowAsync(updatedHotel);
        await ValidateId(updatedHotel.Id);

        updatedHotel.ModificationDate = DateTime.UtcNow;
        await _hotelRepository.UpdateAsync(updatedHotel);
    }

    private async Task ValidateId(Guid Id)
    {
        if (!await ExistsAsync(Id))
            throw new EntityNotFoundException($"Id {Id} Does not exist.");
    }

    public async Task<int> GetNextRoomNumberAsync(Guid hotelId) =>
        await _hotelRepository.GetNextRoomNumberAsync(hotelId);
}