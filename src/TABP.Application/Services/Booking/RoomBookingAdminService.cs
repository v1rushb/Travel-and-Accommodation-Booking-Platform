using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Application.Sorting.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services.Booking;
using TABP.Domain.Models.Booking.Search;
using TABP.Domain.Models.Booking.Search.Response;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.RoomBooking;

namespace TABP.Application.Services.Booking;

public class RoomBookingAdminService : IRoomBookingAdminService
{
    private readonly IRoomBookingRepository _roomBookingRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<RoomBookingAdminService> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<BookingSortQuery> _bookingSortQueryValidator;

    public RoomBookingAdminService(
        IRoomBookingRepository roomBookingRepository,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper,
        ILogger<RoomBookingAdminService> logger,
        ICurrentUserService currentUserService,
        IValidator<BookingSortQuery> bookingSortQueryValidator)
    {
        _roomBookingRepository = roomBookingRepository;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
        _logger = logger;
        _currentUserService = currentUserService;
        _bookingSortQueryValidator = bookingSortQueryValidator;
    }
    
    public async Task<IEnumerable<BookingAdminResponseDTO>> SearchAsync(
        AdminBookingSearchQuery inQuery,
        PaginationDTO pagination,
        BookingSortQuery sortQuery)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        sortQuery.IsAdmin = true;
        _bookingSortQueryValidator.ValidateAndThrow(sortQuery);

        var userId = inQuery.UserId;
        var query = _mapper.Map<BookingSearchQuery>(inQuery);
        
        var filterExpression = BookingExpressionBuilder.Build(query, userId);
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
                Searching for Bookings with query {AdminBookingSearchQuery}
                Sorting: {BookingSortQuery}
                PageNumber: {PageNumber}
                PageSize: {PageSize}
                by User With Id: {UserId}",

                inQuery,
                sortQuery,
                pagination.PageNumber,
                pagination.PageSize,
                _currentUserService.GetUserId());


        return _mapper
            .Map<IEnumerable<BookingAdminResponseDTO>>(bookings);
    }
}