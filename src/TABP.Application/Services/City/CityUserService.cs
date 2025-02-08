using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Application.Filters.ExpressionBuilders.Generics;
using TABP.Application.Sorting.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services.City;
using TABP.Domain.Entities;
using TABP.Domain.Models.City.Search;
using TABP.Domain.Models.City.Sort;
using TABP.Domain.Models.HotelVisit;
using TABP.Domain.Models.Pagination;
using TABP.Models.City;

namespace TABP.Application.Services.City;

public class CityUserService : ICityUserService
{
    private readonly ICityRepository _cityRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<CityUserService> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<CitySortQuery> _sortValidator;
    private readonly IValidator<VisitTimeOptionQuery> _timeOptionsValidator;
    private readonly IHotelVisitRepository _visitsRepository;

    public CityUserService(
        ICityRepository cityRepository,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper,
        ILogger<CityUserService> logger,
        ICurrentUserService currentUserService,
        IValidator<CitySortQuery> sortValidator,
        IValidator<VisitTimeOptionQuery> timeOptionsValidator,
        IHotelVisitRepository visitsRepository)
    {
        _cityRepository = cityRepository;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
        _logger = logger;
        _currentUserService = currentUserService;
        _sortValidator = sortValidator;
        _timeOptionsValidator = timeOptionsValidator;
        _visitsRepository = visitsRepository;
    }

    public async Task<IEnumerable<CitySearchResponseDTO>> SearchAsync(
        CitySearchQuery query,
        PaginationDTO pagination,
        CitySortQuery sortQuery)
    {
        _paginationValidator.ValidateAndThrow(pagination);
        _sortValidator.ValidateAndThrow(sortQuery);


        var expression = CityExpressionBuilder.Build(query);
        var orderByDelegate = CitySortExpressionBuilder
            .GetSortDelegate(sortQuery);

        var cities = await _cityRepository.SearchAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize,
            orderByDelegate
            );

            _logger.LogInformation(
            @"
                Searching For Cities with Search query: {SearchQuery}
                With Sorting Query: {CitySortQuery}
                PageNumber: {PageNumber}
                PageSize: {PageSize}
                By User With Id: {UserId}",

                query,
                sortQuery,
                pagination.PageNumber,
                pagination.PageSize,
                _currentUserService.GetUserId());
        
        return _mapper.Map<IEnumerable<CitySearchResponseDTO>>(cities);
    }

    public async Task<IEnumerable<CityVisitDTO>> GetTrendingCities(
        VisitTimeOptionQuery timeQuery,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);
        _timeOptionsValidator.ValidateAndThrow(timeQuery);

        var filterExpression = TimeOptionExpressionBuilder<HotelVisit>
            .Build(timeQuery);

        var visitedHotels = await _visitsRepository
            .GetVisitedHotels(
                filterExpression,
                pagination.PageNumber,
                pagination.PageSize
            );
        
        var trendyCities = visitedHotels
            .GroupBy(hotel => hotel.CityName)
            .Select(group => new CityVisitDTO
            {
                Name = group.Key,
                Visits = group.Sum(hotel => hotel.Visits)
            });

        return trendyCities;
    }
}