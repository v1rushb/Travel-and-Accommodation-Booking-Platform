using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Entities;
using TABP.Domain.Models.Room;

namespace TABP.Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly ILogger<RoomService> _logger;

    public RoomService(
        IRoomRepository roomRepository,
        ILogger<RoomService> logger)
    {
        _roomRepository = roomRepository;
        _logger = logger;
    }

    public async Task<Guid> AddAsync(RoomDTO newRoom) 
    {
        // do validations here. (confilct and stuff)
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

    public async Task<Room?> GetByIdAsync(Guid Id)
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

    private async Task ValidateId(Guid Id)
    {
        if(!await ExistsAsync(Id))
        {
            throw new KeyNotFoundException($"Id {Id} Does not exist.");
        }
    }

}