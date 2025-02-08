using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Application.Sorting.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services.City;
using TABP.Domain.Models.City.Search;
using TABP.Domain.Models.City.Sort;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services.City;

public class CityUserService : ICityUserService
{
    private readonly ICityRepository _cityRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<CityUserService> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<CitySortQuery> _sortValidator;

    public CityUserService(
        ICityRepository cityRepository,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper,
        ILogger<CityUserService> logger,
        ICurrentUserService currentUserService,
        IValidator<CitySortQuery> sortValidator)
    {
        _cityRepository = cityRepository;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
        _logger = logger;
        _currentUserService = currentUserService;
        _sortValidator = sortValidator;
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
}