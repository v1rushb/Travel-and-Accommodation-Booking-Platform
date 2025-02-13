using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Application.Sorting.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services.Booking;
using TABP.Domain.Abstractions.Utilities.Injectable;
using TABP.Domain.Models.Booking.Search;
using TABP.Domain.Models.Booking.Search.Response;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Application.Services.Booking;

public class RoomBookingUserService : IRoomBookingUserService
{
    private readonly IRoomBookingRepository _roomBookingRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<RoomBookingUserService> _logger;
    private readonly IValidator<BookingSortQuery> _bookingSortQueryValidator;

    public RoomBookingUserService(
        IRoomBookingRepository roomBookingRepository,
        ICurrentUserService currentUserService,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper,
        ILogger<RoomBookingUserService> logger,
        IValidator<BookingSortQuery> bookingSortQueryValidator)
    {
        _roomBookingRepository = roomBookingRepository;
        _currentUserService = currentUserService;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
        _logger = logger;
        _bookingSortQueryValidator = bookingSortQueryValidator;
    }
    public async Task<IEnumerable<BookingUserResponseDTO>> SearchAsync(
        BookingSearchQuery query,
        PaginationDTO pagination,
        BookingSortQuery sortQuery)
    {
        _paginationValidator.ValidateAndThrow(pagination);
        _bookingSortQueryValidator.ValidateAndThrow(sortQuery);

        var currentUserId = _currentUserService.GetUserId();
        var filterExpression = BookingExpressionBuilder.Build(query, currentUserId);
        var orderByDelegate = BookingSortExpressionBuilder
            .GetSortDelegate(sortQuery);

        var bookings = await _roomBookingRepository.SearchAsync(
            filterExpression,
            pagination.PageNumber,
            pagination.PageSize,
            orderByDelegate
        );

        _logger.LogInformation(
            @"
                Searching for Bookings with query {BookingSearchQuery}
                Sorting: {BookingSortQuery}
                PageNumber: {PageNumber}
                PageSize: {PageSize}
                by User With Id: {UserId}",

                query,
                sortQuery,
                pagination.PageNumber,
                pagination.PageSize,
                _currentUserService.GetUserId());
        
        return _mapper.Map<IEnumerable<BookingUserResponseDTO>>(bookings);
    }
}