using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services.City;
using TABP.Domain.Models.City.Search;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services.City;

public class CityUserService : ICityUserService
{
    private readonly ICityRepository _cityRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<CityUserService> _logger;
    private readonly ICurrentUserService _currentUserService;

    public CityUserService(
        ICityRepository cityRepository,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper,
        ILogger<CityUserService> logger,
        ICurrentUserService currentUserService)
    {
        _cityRepository = cityRepository;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<CitySearchResponseDTO>> SearchAsync(CitySearchQuery query, PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var expression = CityExpressionBuilder.Build(query);
        var cities = await _cityRepository.SearchAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);

        _logger.LogInformation(
            "Searching for Cities with query {@CitySearchQuery} by User {UserId}", 
            query, 
            _currentUserService.GetUserId());
        
        return _mapper.Map<IEnumerable<CitySearchResponseDTO>>(cities);
    }
}