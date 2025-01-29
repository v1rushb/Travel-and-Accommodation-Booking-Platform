using AutoMapper;
using FluentValidation;
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

    public CityUserService(
        ICityRepository cityRepository,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper)
    {
        _cityRepository = cityRepository;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CitySearchResponseDTO>> SearchAsync(CitySearchQuery query, PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var expression = CityExpressionBuilder.Build(query);
        var cities = await _cityRepository.SearchAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);
        
        return _mapper.Map<IEnumerable<CitySearchResponseDTO>>(cities);
    }
}