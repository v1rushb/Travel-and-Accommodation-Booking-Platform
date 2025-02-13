using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Application.Sorting.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services.City;
using TABP.Domain.Abstractions.Utilities.Injectable;
using TABP.Domain.Models.City.Response;
using TABP.Domain.Models.City.Search;
using TABP.Domain.Models.City.Sort;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services.City;

public class CityAdminService : ICityAdminService
{
    private readonly ICityRepository _cityRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<CityAdminService> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<CitySortQuery> _citySortQueryValidator;

    public CityAdminService(
        ICityRepository cityRepository,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper,
        ILogger<CityAdminService> logger,
        ICurrentUserService currentUserService,
        IValidator<CitySortQuery> citySortQueryValidator)
    {
        _cityRepository = cityRepository;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
        _logger = logger;
        _currentUserService = currentUserService;
        _citySortQueryValidator = citySortQueryValidator;
    }
    public async Task<IEnumerable<CityAdminResponseDTO>> SearchAsync(
        CitySearchQuery query,
        PaginationDTO pagination,
        CitySortQuery sortQuery)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        sortQuery.IsAdmin = true;
        _citySortQueryValidator.ValidateAndThrow(sortQuery);
        
        var filterExpression = CityExpressionBuilder.Build(query);
        var orderByDelegate = CitySortExpressionBuilder
            .GetSortDelegate(sortQuery);

        var cities = await _cityRepository.SearchAsync(
            filterExpression,
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

        return _mapper.Map<IEnumerable<CityAdminResponseDTO>>(cities);
    }
}