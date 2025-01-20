using System.Xml.Serialization;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Entities;
using TABP.Domain.Models.Hotels;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Room;
using TABP.Domain.Models.Room.Search;
using TABP.Domain.Models.Room.Search.Response;

namespace TABP.Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly ILogger<RoomService> _logger;
    private readonly IValidator<RoomDTO> _roomValidator;
    private readonly IHotelService _hotelService;

    public RoomService(
        IRoomRepository roomRepository,
        ILogger<RoomService> logger,
        IValidator<RoomDTO> roomValidator,
        IHotelService hotelService)
    {
        _roomRepository = roomRepository;
        _logger = logger;
        _roomValidator = roomValidator;
        _hotelService = hotelService;
    }

    public async Task<Guid> AddAsync(RoomDTO newRoom) 
    {
        await _roomValidator.ValidateAndThrowAsync(newRoom);

        var nextRoomNumber = await _hotelService.GetNextRoomNumberAsync(newRoom.HotelId);
        newRoom.Number = nextRoomNumber;
        var roomId = await _roomRepository.AddAsync(newRoom);

        _logger.LogInformation("Added Room: {Number}, HotelId: {HotelId}, Id: {Id}", newRoom.Number, newRoom.HotelId, roomId);
        return roomId;
    }

    public async Task DeleteAsync(Guid Id)
    {
        await ValidateId(Id);
        await _roomRepository.DeleteAsync(Id);
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _roomRepository.ExistsAsync(Id);

    public async Task<RoomDTO?> GetByIdAsync(Guid Id)
    {
        await ValidateId(Id);

        return await _roomRepository.GetByIdAsync(Id);
    }

    public async Task<IEnumerable<Room>> GetRoomsByHotelAsync(Guid hotelId) =>
        await _roomRepository.GetRoomsByHotelAsync(hotelId);
        
    public async Task<bool> RoomNumberExistsForHotelAsync(Guid hotelId, Guid RoomId) =>
        await _roomRepository.RoomExistsForHotelAsync(hotelId, RoomId);

    public async Task UpdateAsync(RoomDTO updatedRoom)
    {
        //some validations here.

        updatedRoom.ModificationDate = DateTime.UtcNow;
        await _roomRepository.UpdateAsync(updatedRoom);
    }

    public async Task<IEnumerable<RoomAdminResponseDTO>> SearchAdminAsync(RoomSearchQuery query, PaginationDTO pagination)
    {
        var expression = RoomForAdminExpressionBuilder.Build(query);
        return await _roomRepository.SearchAdminAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);
    }

    private async Task ValidateId(Guid Id)
    {
        if(!await ExistsAsync(Id))
        {
            throw new KeyNotFoundException($"Id {Id} Does not exist.");
        }
    }

}