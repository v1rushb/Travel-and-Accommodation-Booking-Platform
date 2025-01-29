using FluentValidation;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services.Hotel;
using TABP.Domain.Models.Hotel;

namespace TABP.Domain.Abstractions.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IValidator<HotelDTO> _hotelValidator;
    private readonly IImageService _imageService;

    public HotelService(
        IHotelRepository hotelRepository,
        IValidator<HotelDTO> hotelValidator,
        IImageService imageService)
    {
        _hotelRepository = hotelRepository;
        _hotelValidator = hotelValidator;
        _imageService = imageService;
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

    public async Task<HotelDTO> GetByIdAsync(Guid Id) {
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

    public async Task<int> GetNextRoomNumberAsync(Guid hotelId) =>
        await _hotelRepository.GetNextRoomNumberAsync(hotelId);
}