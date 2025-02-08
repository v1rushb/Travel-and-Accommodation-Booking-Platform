using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Domain.Utilities;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Exceptions;
using TABP.Domain.Models.Room;

namespace TABP.Application.Services.Room;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly ILogger<RoomService> _logger;
    private readonly IValidator<RoomDTO> _roomValidator;
    private readonly IHotelRepository _hotelRepository;
    private readonly IDiscountRepository _discountRepository;

    public RoomService(
        IRoomRepository roomRepository,
        ILogger<RoomService> logger,
        IValidator<RoomDTO> roomValidator,
        IHotelRepository hotelRepository,
        IDiscountRepository discountRepository)
    {
        _roomRepository = roomRepository;
        _logger = logger;
        _roomValidator = roomValidator;
        _hotelRepository = hotelRepository;
        _discountRepository = discountRepository;
    }

    public async Task AddAsync(RoomDTO newRoom)
    {
        await _roomValidator.ValidateAndThrowAsync(newRoom);

        var nextRoomNumber = await _hotelRepository.GetNextRoomNumberAsync(newRoom.HotelId);

        newRoom.Number = nextRoomNumber;
        var roomId = await _roomRepository.AddAsync(newRoom);

        _logger.LogInformation("Added Room: {Number}, HotelId: {HotelId}, Id: {Id}", newRoom.Number, newRoom.HotelId, roomId);
    }

    public async Task DeleteAsync(Guid Id)
    {
        await ValidateId(Id);
        await _roomRepository.DeleteAsync(Id);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _roomRepository.ExistsAsync(Id);

    public async Task<RoomDTO> GetByIdAsync(Guid Id)
    {
        await ValidateId(Id);

        return await _roomRepository.GetByIdAsync(Id);
    }

    public async Task<bool> RoomNumberExistsForHotelAsync(Guid hotelId, Guid RoomId) =>
        await _roomRepository.RoomExistsForHotelAsync(hotelId, RoomId);

    public async Task UpdateAsync(RoomDTO updatedRoom)
    {
        await _roomValidator.ValidateAndThrowAsync(updatedRoom);
        await ValidateId(updatedRoom.Id);

        updatedRoom.ModificationDate = DateTime.UtcNow;
        await _roomRepository.UpdateAsync(updatedRoom);
    }

    private async Task ValidateId(Guid Id)
    {
        if (!await ExistsAsync(Id))
        {
            throw new EntityNotFoundException($"Id {Id} Does not exist.");
        }
    }

    public async Task<decimal> GetBookingPriceForRoom(Guid roomId, DateTime checkInDate, DateTime checkOutDate)
    {
        var room = await GetByIdAsync(roomId);

        var discount = await _discountRepository
            .GetHighestDiscountActiveForHotelRoomTypeAsync(room.HotelId, room.Type);

        return DiscountedPriceCalculator.GetFinalDiscountedPrice(
            checkInDate,
            checkOutDate,
            room.PricePerNight,
            discount.AmountPercentage);
    }
}