using System.Linq.Expressions;
using FluentValidation;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Application.Filters.ExpressionBuilders.Generics;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.Hotel.Search;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.Hotels;
using TABP.Domain.Models.HotelVisit;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IValidator<HotelDTO> _hotelValidator;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IDiscountRepository _discountRepository;
    private readonly IHotelVisitService _hotelVisitService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IRoomBookingService _roomBookingService;

    public HotelService(
        IHotelRepository hotelRepository,
        IValidator<HotelDTO> hotelValidator,
        IValidator<PaginationDTO> paginationValidator,
        IDiscountRepository discountRepository,
        IHotelVisitService hotelVisitService,
        ICurrentUserService currentUserService,
        IRoomBookingService roomBookingService)
    {
        _hotelRepository = hotelRepository;
        _hotelValidator = hotelValidator;
        _paginationValidator = paginationValidator;
        _discountRepository = discountRepository;
        _hotelVisitService = hotelVisitService;
        _currentUserService = currentUserService;
        _roomBookingService = roomBookingService;
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

    public async Task<IEnumerable<HotelUserResponseDTO>> SearchAsync(
        HotelSearchQuery query,
        PaginationDTO pagination) 
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var expression = HotelExpressionBuilder.Build(query);
        return await _hotelRepository.SearchAsync(expression, pagination.PageNumber, pagination.PageSize);
    }

    private async Task ValidateId(Guid Id)
    {
        if(!await ExistsAsync(Id))
            throw new KeyNotFoundException($"Id {Id} Does not exist.");
    }

    public async Task<IEnumerable<HotelAdminResponseDTO>> SearchAdminAsync(
        HotelSearchQuery query,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var expression = HotelExpressionBuilder.Build(query);
        return await _hotelRepository.SearchAdminAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);
    }

    public async Task<int> GetNextRoomNumberAsync(Guid hotelId) =>
        await _hotelRepository.GetNextRoomNumberAsync(hotelId);

    public async Task<HotelPageResponseDTO> GetHotelPageAsync(Guid hotelId)
    {
        await ValidateId(hotelId);

        var hotel = await _hotelRepository
            .GetHotelPageAsync(hotelId);
        
        var discounts = await _discountRepository
            .GetActiveDiscountsForHotelAsync(hotelId);
        
        hotel.Discounts = discounts;

        await _hotelVisitService.AddAsync(new HotelVisitDTO
        {
            CreationDate = DateTime.UtcNow,
            HotelId = hotelId,
            UserId = _currentUserService
                .GetUserId()
        });

        return hotel;
    }

    public async Task<IEnumerable<FeaturedHotelDTO>> GetWeeklyFeaturedHotelsAsync()
    {
        var timeOption = (int) TimeOptions.LastWeek;
        var expression = TimeOptionExpressionBuilder<Hotel>
            .Build(new VisitTimeOptionQuery 
            {
                TimeOption = timeOption
            });

        var mostVisitedHotels = await _hotelRepository
            .GetWeeklyFeaturedHotelsAsync(expression);
        
        var hotelIds = mostVisitedHotels
            .Select(hotel => hotel.Id)
            .ToList();

        var bookings = await _roomBookingService
            .GetByHotelAsync();

        var bookingCountByHotel = bookings
            .Where(booking => hotelIds.Contains(booking.HotelId))
            .GroupBy(booking => booking.HotelId)
            .ToDictionary(group => group.Key, group => group.Count());
        
        var featuredHotels = mostVisitedHotels
            .Select(hotel => new FeaturedHotelDTO {
                Name = hotel.Name,
                HotelId = hotel.Id,
                StarRating = hotel.StarRating,
                Bookings = bookingCountByHotel.TryGetValue(hotel.Id, out int value) ? value : 0

            })
            .OrderByDescending(h => h.Bookings)
            .ThenByDescending(h => h.StarRating)
            .ToList();

        return featuredHotels;
    }
    public async Task<IEnumerable<HotelHistoryDTO>> GetHotelHistoryAsync(
        PaginationDTO pagination,
        VisitTimeOptionQuery query,
        Guid? userId = null) 
    {
        var targetUserId = userId ?? _currentUserService.GetUserId();

        var expression = TimeOptionExpressionBuilder<HotelVisit>
            .Build(new VisitTimeOptionQuery 
            {
                TimeOption = query.TimeOption ?? (int) TimeOptions.AllTime
            });

        return await _hotelRepository
            .GetHotelHistoryAsync(
                expression,
                targetUserId,
                pagination.PageNumber,
                pagination.PageSize
            );
    }
}