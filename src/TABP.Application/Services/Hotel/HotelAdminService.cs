using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services.Hotel;
using TABP.Domain.Models.Hotel.Search;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services.Hotel;

public class HotelAdminService : IHotelAdminService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<HotelAdminService> _logger;
    private readonly ICurrentUserService _currentUserService;

    public HotelAdminService(
        IHotelRepository hotelRepository,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper,
        ILogger<HotelAdminService> logger,
        ICurrentUserService currentUserService)

    {
        _hotelRepository = hotelRepository;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<HotelAdminResponseDTO>> SearchAsync(
        HotelSearchQuery query,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var expression = HotelExpressionBuilder.Build(query);
        var hotels = await _hotelRepository.SearchAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);

        _logger.LogInformation(
            "Searching for Hotels with query {@HotelSearchQuery} by User {UserId}",
            query,
            _currentUserService.GetUserId());

        return _mapper.Map<IEnumerable<HotelAdminResponseDTO>>(hotels);
    }
}