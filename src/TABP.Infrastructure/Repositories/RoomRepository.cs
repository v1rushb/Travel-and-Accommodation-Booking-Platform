using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Utilities;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Models.Room;
using TABP.Infrastructure.Extensions.Helpers;

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

         _logger.LogInformation(
            @"Created Room: {Number} With Id: {RoomId}
            for Hotel with Id: {HotelId}",

            newRoom.Number,
            entityEntry.Entity.Id,
            newRoom.HotelId
        );
        return entityEntry.Entity.Id;
    }

    public async Task DeleteAsync(Guid Id) 
    {
        _context.Rooms.Remove(new Room { Id = Id});

        await _context.SaveChangesAsync();

        _logger.LogInformation("Deleted Room with Id: {Id}", 
            Id
        );
    }

    public async Task<bool> ExistsAsync(Guid Id) =>
        await _context.Rooms.AnyAsync(room => room.Id == Id);

    public async Task<RoomDTO> GetByIdAsync(Guid Id) =>
        _mapper.Map<RoomDTO>(await _context.Rooms
            .FirstOrDefaultAsync(room => room.Id == Id));

    public async Task<IEnumerable<Room>> GetRoomsByHotelAsync(Guid HotelId) =>
        await _context.Rooms.Where(room => room.HotelId == HotelId).ToListAsync();
        

    public async Task UpdateAsync(RoomDTO updatedRoom)
    {
        var room = _mapper.Map<Room>(updatedRoom); 
        _context.Rooms.Update(room);
        await _context.SaveChangesAsync();
        _logger.LogInformation(
            @"Updated Room: {Number} With Id: {Id}",

            updatedRoom.Number,
            updatedRoom.Id); 
    }

    public async Task<bool> RoomExistsForHotelAsync(Guid HotelId, Guid RoomId) =>
        await _context.Rooms.AnyAsync(room => room.HotelId == HotelId && room.Id == RoomId);

    public async Task<IEnumerable<RoomDTO>> SearchAsync(
        Expression<Func<AvailableRoom, bool>> predicate,
        int pageNumber,
        int pageSize,
        Func<IQueryable<AvailableRoom>, IOrderedQueryable<AvailableRoom>> orderBy = null)
    {
        var rooms = await _context.AvailableRooms
            .Where(predicate)
            .OrderByIf(orderBy != null, orderBy)
            .PaginateAsync(
                pageNumber,
                pageSize
            );
        
        var mappedRooms = _mapper
            .Map<IEnumerable<RoomDTO>>(rooms);

        await SetRoomDiscountedPrice(mappedRooms);

        return mappedRooms;
    }

    private async Task SetRoomDiscountedPrice(IEnumerable<RoomDTO> rooms)
    {
        var hotelIds = rooms
            .Select(room => room.HotelId)
            .Distinct()
            .ToList();

        var discountData = await _context.Discounts
            .Where(discount => hotelIds.Contains(discount.HotelId) && 
                discount.StartingDate <= DateTime.UtcNow && discount.EndingDate >= DateTime.UtcNow)
            .GroupBy(discount => discount.HotelId)
            .Select(group => new 
                {
                    HotelId = group.Key,
                    Discount = group.OrderByDescending(discount => discount.AmountPercentage)
                        .FirstOrDefault()
                })
            .ToListAsync();

        var discounts = discountData
            .ToDictionary(
                discount => discount.HotelId,
                discount => discount.Discount?.AmountPercentage 
                    ?? 0);

        var discountTasks = rooms.Select(room => 
        {
            var discountPercentage = discounts
                .GetValueOrDefault(room.HotelId, 0);

            room.PricePerNight = (int) DiscountedPriceCalculator.GetFinalDiscountedPrice(
                DateTime.UtcNow.AddMinutes(-10),
                DateTime.UtcNow.AddDays(1),
                room.PricePerNight,
                discountPercentage);

            return Task.CompletedTask;
        });

        await Task.WhenAll(discountTasks);
    }
}