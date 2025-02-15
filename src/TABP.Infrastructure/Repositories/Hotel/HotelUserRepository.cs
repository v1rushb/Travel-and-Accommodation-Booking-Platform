using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Entities;
using TABP.Domain.Hotels;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Infrastructure.Extensions.Helpers;
using TABP.Domain.Rooms;
using TABP.Domain.Utilities;

namespace TABP.Infrastructure.Repositories;

public class HotelUserRepository : IHotelUserRepository
{
    private readonly HotelBookingDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<HotelUserRepository> _logger;

    public HotelUserRepository(
        HotelBookingDbContext context,
        IMapper mapper,
        ILogger<HotelUserRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<HotelPageResponseDTO> GetHotelPageAsync(Guid hotelId)
    {
        var hotel = await _context.Hotels
            .Include(hotel => hotel.City)
            .Where(hotel => hotel.Id == hotelId)
            .FirstOrDefaultAsync();

        _logger.LogInformation("Hotel Page for Hotel Id {HotelId} requested", hotelId);

        return _mapper.Map<HotelPageResponseDTO>(hotel);
    }
    
    public async Task<IEnumerable<VisitedHotelDTO>> GetWeeklyFeaturedHotelsAsync(
        Expression<Func<HotelVisit, bool>> predicate)
    {
        var hotels = await _context.Hotels
            .Select(hotel => new VisitedHotelDTO
            {
                Id = hotel.Id,
                Visits = hotel.HotelVisits
                    .AsQueryable()
                    .Count(predicate),
                UniqueVisitors = hotel.HotelVisits
                    .Select(visit => visit.UserId)
                    .Distinct()
                    .Count(),
                StarRating = hotel.StarRating,
                Name = hotel.Name
            })
            .OrderByDescending(hotel => hotel.Visits)
            .ThenByDescending(hotel => hotel.StarRating)
            .Take(5)
            .ToListAsync();

        _logger.LogInformation("Weekly Featured Hotels requested");

        return hotels;
    }

        public async Task<IEnumerable<HotelHistoryDTO>> GetHotelHistoryAsync(
        Expression<Func<HotelVisit, bool>> predicate,
        Guid userId, 
        int pageNumber, 
        int pageSize)
    {
        var query = await _context.Hotels
        .Where(hotel => hotel.HotelVisits
            .Any(visit => visit.UserId == userId))
        .Select(hotel => new
        {
            Hotel = hotel,
            Visits = hotel.HotelVisits
                .Where(visit => visit.UserId == userId)
        })
        .ToListAsync();

        var filtered = query
            .Select(hotel => new HotelHistoryDTO{
                HotelId = hotel.Hotel.Id,
                Name = hotel.Hotel.Name,
                BriefDescription = hotel.Hotel.BriefDescription,
                StarRating = hotel.Hotel.StarRating,
                LastVisitDate = hotel.Visits
                    .Where(predicate.Compile())
                    .OrderByDescending(visit => visit.CreationDate)
                    .Select(visit => visit.CreationDate)
                    .FirstOrDefault()
            })
            .Where(hotel => hotel.LastVisitDate != default)
            .OrderByDescending(hotel => hotel.LastVisitDate)
            .PaginateAsync(
                pageNumber,
                pageSize);

        _logger.LogInformation("Hotel History requested for User {UserId}, history count: {Count}", userId, filtered.Count());

        return filtered;
    }

    public async Task<IEnumerable<HotelDealDTO>> GetDealsAsync()
    {
        var discountData = await _context.Discounts
            .Where(discount => discount.StartingDate <= DateTime.UtcNow &&
                                discount.EndingDate >= DateTime.UtcNow)
            .GroupBy(discount => new { discount.HotelId, discount.roomType })
            .Select(group => new
            {
                group.Key.HotelId,
                group.Key.roomType,
                Discount = group.OrderByDescending(discount => discount.AmountPercentage)
                    .FirstOrDefault()
            })
            .ToListAsync();

        var discountLookup = discountData.ToDictionary(
                key => (key.HotelId, key.roomType),
                value => value.Discount);

        var hotels = await _context.Hotels
            .Include(hotel => hotel.City)
            .Include(hotel => hotel.Rooms)
            .Where(hotel => hotel.Rooms.Count() != 0)
            .ToListAsync();

         var hotelRoomCombos = hotels.SelectMany(hotel =>
            hotel.Rooms
                .Where(room => discountLookup.ContainsKey((hotel.Id, room.Type)))
                .Select(room => new
                {
                    Hotel = hotel,
                    Room = room,
                    Discount = discountLookup[(hotel.Id, room.Type)]
                }))
            .ToList();

        var hotelDeals = hotelRoomCombos
        .GroupBy(combo => combo.Hotel)
        .Select(group => new HotelDealDTO
        {
            Id = group.Key.Id,
            HotelName = group.Key.Name,
            HotelRating = group.Key.StarRating,
            BriefDescription = group.Key.BriefDescription,
            CityName = group.Key.City.Name,
            Rooms = group.Select(combo =>
            {
                var discountPercentage = combo.Discount.AmountPercentage;
                return new RoomDealDTO
                {
                    Id = combo.Room.Id,
                    RoomType = combo.Room.Type.ToString(),
                    OriginalPricePerNight = combo.Room.PricePerNight,
                    DiscountPercentage = discountPercentage,
                    FinalPricePerNight = DiscountedPriceCalculator.GetFinalDiscountedPrice(
                        DateTime.UtcNow.AddMinutes(-10),
                        DateTime.UtcNow.AddDays(-10),
                        combo.Room.PricePerNight,
                        discountPercentage),
                    EndDate = combo.Discount.EndingDate
                };
            })
            .OrderBy(room => room.FinalPricePerNight)
            .ThenByDescending(room => room.DiscountPercentage)
            .ToList()
        })
        .OrderBy(hotel => 
            hotel.Rooms
            .FirstOrDefault()?.FinalPricePerNight 
                ?? decimal.MaxValue)
        .ThenByDescending(hotel => 
            hotel.Rooms
            .FirstOrDefault()?.DiscountPercentage
                ?? 0)
        .ThenBy(hotel => hotel.HotelRating)
        .ToList();

        return hotelDeals;
    }
}