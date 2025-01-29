using AutoMapper;
using FluentValidation;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services.Booking;
using TABP.Domain.Models.Booking.Search;
using TABP.Domain.Models.Booking.Search.Response;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services.Booking;

public class RoomBookingUserService : IRoomBookingUserService
{
    private readonly IRoomBookingRepository _roomBookingRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;

    public RoomBookingUserService(
        IRoomBookingRepository roomBookingRepository,
        ICurrentUserService currentUserService,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper)
    {
        _roomBookingRepository = roomBookingRepository;
        _currentUserService = currentUserService;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
    }
    public async Task<IEnumerable<BookingUserResponseDTO>> SearchAsync(
        BookingSearchQuery query,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var currentUserId = _currentUserService.GetUserId();
        var expression = BookingExpressionBuilder.Build(query, currentUserId);

        var bookings = await _roomBookingRepository.SearchAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);
        
        return _mapper.Map<IEnumerable<BookingUserResponseDTO>>(bookings);
    }
}