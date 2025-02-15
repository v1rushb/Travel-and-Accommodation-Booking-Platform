using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Application.Filters.ExpressionBuilders.Generics;
using TABP.Application.Sorting.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Abstractions.Services.Booking;
using TABP.Domain.Abstractions.Utilities.Injectable;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Exceptions;
using TABP.Domain.Hotels;
using TABP.Domain.Models.Discount;
using TABP.Domain.Models.Hotel;
using TABP.Domain.Models.Hotel.Search;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.Hotel.Sort;
using TABP.Domain.Models.HotelVisit;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services.Hotel;

public class HotelUserService : IHotelUserService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IHotelUserRepository _hotelUserRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IHotelVisitRepository _hotelVisitService;
    private readonly IRoomBookingService _roomBookingService;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ILogger<HotelUserService> _logger;
    private readonly IValidator<HotelSortQuery> _hotelSortQueryValidator;
    private readonly IValidator<VisitTimeOptionQuery> _timeOptionsValidator;

    public HotelUserService(
        IHotelRepository hotelRepository,
        IValidator<PaginationDTO> paginationValidator,
        IHotelUserRepository hotelUserRepository,
        IDiscountRepository discountRepository,
        IHotelVisitRepository hotelVisitService,
        ICurrentUserService currentUserService,
        IRoomBookingService roomBookingService,
        IMapper mapper,
        ILogger<HotelUserService> logger,
        IValidator<HotelSortQuery> hotelSortQueryValidator,
        IValidator<VisitTimeOptionQuery> timeOptionsValidator)
    {
        _hotelRepository = hotelRepository;
        _paginationValidator = paginationValidator;
        _hotelUserRepository = hotelUserRepository;
        _discountRepository = discountRepository;
        _hotelVisitService = hotelVisitService;
        _currentUserService = currentUserService;
        _roomBookingService = roomBookingService;
        _mapper = mapper;
        _logger = logger;
        _hotelSortQueryValidator = hotelSortQueryValidator;
        _timeOptionsValidator = timeOptionsValidator;
    }

    public async Task<IEnumerable<HotelUserResponseDTO>> SearchAsync(
        HotelSearchQuery query,
        PaginationDTO pagination,
        HotelSortQuery sortQuery)
    {
        _paginationValidator.ValidateAndThrow(pagination);
        _hotelSortQueryValidator.ValidateAndThrow(sortQuery);


        var filterExpression = HotelExpressionBuilder.Build(query);
        var orderBy = HotelSortExpressionBuilder.GetSortDelegate(sortQuery);
        var hotels = await _hotelRepository
            .SearchAsync(
                filterExpression,
                pagination.PageNumber,
                pagination.PageSize,
                null,
                orderBy
            );
        

        _logger.LogInformation(
            @"Searching for Hotels with query {HotelSearchQuery},
            Sorting: {SortQuery},
            Page Number: {PageNumber},
            Page Size: {PageSize}
            By User with Id: {UserId}",
            
            query,
            sortQuery,
            pagination.PageNumber,
            pagination.PageSize,
            _currentUserService.GetUserId());

        return _mapper.Map<IEnumerable<HotelUserResponseDTO>>(hotels);
    }

    public async Task<HotelPageResponseDTO> GetHotelPageAsync(Guid hotelId)
    {
        await ValidateId(hotelId);

        var hotel = await _hotelUserRepository
            .GetHotelPageAsync(hotelId);

        var discounts = await _discountRepository
            .GetActiveDiscountsForHotelAsync(hotelId);

        hotel.Discounts = _mapper.Map<IEnumerable<DiscountUserResponseDTO>>(discounts);

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
        var timeOption = TimeOptions.LastWeek;
        var expression = TimeOptionExpressionBuilder<HotelVisit>
            .Build(new VisitTimeOptionQuery
            {
                TimeOption = Enum.Parse(typeof(TimeOptions), timeOption.ToString())
                    .ToString()
            });

        var mostVisitedHotels = await _hotelUserRepository
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
            .Select(hotel => new FeaturedHotelDTO
            {
                Id = hotel.Id,
                Name = hotel.Name,
                WeeklyVisits = hotel.Visits,
                UniqueVisitors = hotel.UniqueVisitors,
                StarRating = hotel.StarRating,
                WeeklyBookings = bookingCountByHotel
                    .TryGetValue(hotel.Id, out int value) ? value : 0

            })
            .OrderByDescending(hotel => hotel.WeeklyBookings)
            .ThenByDescending(hotel => hotel.WeeklyVisits)
            .ThenByDescending(hotel => hotel.StarRating)
            .ToList();

        return featuredHotels;
    }
    public async Task<IEnumerable<HotelHistoryDTO>> GetHotelHistoryAsync(
        PaginationDTO pagination,
        VisitTimeOptionQuery query,
        Guid? userId = null)
    {
        _timeOptionsValidator.ValidateAndThrow(query);
        
        var targetUserId = userId ?? _currentUserService.GetUserId();

        var expression = TimeOptionExpressionBuilder<HotelVisit>
            .Build(new VisitTimeOptionQuery
            {
                TimeOption = query.TimeOption
            });

        return await _hotelUserRepository
            .GetHotelHistoryAsync(
                expression,
                targetUserId,
                pagination.PageNumber,
                pagination.PageSize
            );
    }


    private async Task ValidateId(Guid Id)
    {
        if (!await _hotelRepository.ExistsAsync(Id))
            throw new EntityNotFoundException($"Id {Id} Does not exist.");
    }

    public async Task<IEnumerable<HotelDealDTO>> GetDealsAsync() =>
        await _hotelUserRepository.GetDealsAsync();
}