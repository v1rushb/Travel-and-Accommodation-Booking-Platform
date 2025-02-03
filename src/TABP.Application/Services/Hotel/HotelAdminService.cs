using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Application.Sorting.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services.Hotel;
using TABP.Domain.Models.Hotel.Search;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.Hotel.Sort;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services.Hotel;

public class HotelAdminService : IHotelAdminService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<HotelAdminService> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<HotelSortQuery> _sortQueryValidator;

    public HotelAdminService(
        IHotelRepository hotelRepository,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper,
        ILogger<HotelAdminService> logger,
        ICurrentUserService currentUserService,
        IValidator<HotelSortQuery> sortQueryValidator)

    {
        _hotelRepository = hotelRepository;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
        _logger = logger;
        _currentUserService = currentUserService;
        _sortQueryValidator = sortQueryValidator;
    }

    public async Task<IEnumerable<HotelAdminResponseDTO>> SearchAsync(
        HotelSearchQuery query,
        PaginationDTO pagination,
        HotelSortQuery sortQuery)
    {
        sortQuery.IsAdmin = true;

        _paginationValidator.ValidateAndThrow(pagination);
        _sortQueryValidator.ValidateAndThrow(sortQuery);

        var filterExpression = HotelExpressionBuilder.Build(query);

        var sortByDelegate = HotelSortExpressionBuilder
            .GetSortDelegate(sortQuery);


        var hotels = await _hotelRepository.SearchAsync(
            filterExpression,
            pagination.PageNumber,
            pagination.PageSize,
            sortByDelegate
        );

        _logger.LogInformation(
            @"Searching for Hotels with query {HotelSearchQuery},
            {HotelSortQuery}, 
            PageNumber: {PageNumber}, 
            PageSize: {PageSize} By User {UserId}",

            query,
            sortByDelegate,
            pagination.PageNumber,
            pagination.PageSize,
            _currentUserService.GetUserId());

        return _mapper.Map<IEnumerable<HotelAdminResponseDTO>>(hotels);
    }
}