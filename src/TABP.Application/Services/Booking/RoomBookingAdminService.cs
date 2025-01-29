using AutoMapper;
using FluentValidation;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services.Booking;
using TABP.Domain.Models.Booking.Search;
using TABP.Domain.Models.Booking.Search.Response;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services.Booking;

public class RoomBookingAdminService : IRoomBookingAdminService
{
    private readonly IRoomBookingRepository _roomBookingRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;

    public RoomBookingAdminService(
        IRoomBookingRepository roomBookingRepository,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper)
    {
        _roomBookingRepository = roomBookingRepository;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<BookingAdminResponseDTO>> SearchAsync(
        AdminBookingSearchQuery inQuery,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var userId = inQuery.UserId;
        var query = _mapper.Map<BookingSearchQuery>(inQuery);
        
        var expression = BookingExpressionBuilder.Build(query, userId);

        var bookings = await _roomBookingRepository.SearchAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);

        return _mapper.Map<IEnumerable<BookingAdminResponseDTO>>(bookings);
    }
}