using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Models.Room;

namespace TABP.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly ILogger<RoomRepository> _logger;
    private readonly IMapper _mapper;

    public RoomRepository(
        HotelBookingDbContext context,
        ILogger<RoomRepository> logger,
        IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Guid> AddAsync(RoomDTO newRoom)
    {
        var room = _mapper.Map<Room>(newRoom);
        room.CreationDate = DateTime.UtcNow;
        room.ModificationDate = DateTime.UtcNow;

        var entityEntry = _context.Rooms.Add(room);
        await _context.SaveChangesAsync();

         _logger.LogInformation("Added Room: {Number}, HotelId: {HotelId}", entityEntry.Entity.Number, entityEntry.Entity.HotelId);
        return entityEntry.Entity.Id;
    }

    public async Task DeleteAsync(Guid Id) 
    {
        _context.Rooms.Remove(new Room { Id = Id});

        await _context.SaveChangesAsync();

        _logger.LogInformation($"Room with Id: {Id} has been deleted");
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _context.Rooms.AnyAsync(room => room.Id == Id);

    public async Task<Room?> GetByIdAsync(Guid Id) =>
        await _context.Rooms.FirstOrDefaultAsync(room => room.Id == Id);

    public async Task<IEnumerable<Room>> GetRoomsByHotelAsync(Guid HotelId) =>
        await _context.Rooms.Where(room => room.HotelId == HotelId).ToListAsync();
        

    public async Task UpdateAsync(RoomDTO updatedRoom)
    {
        var room = _mapper.Map<Room>(updatedRoom); 
        _context.Rooms.Update(room);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Updated Room: {Number}, Id: {Id}", updatedRoom.Number, updatedRoom.Id);
    }

    public async Task<bool> RoomExistsForHotelAsync(Guid HotelId, Guid RoomId) =>
        await _context.Rooms.AnyAsync(room => room.HotelId == HotelId && room.Id == RoomId);
}