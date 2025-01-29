using AutoMapper;
using FluentValidation;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services.Hotel;
using TABP.Domain.Models.Hotel.Search;
using TABP.Domain.Models.Hotel.Search.Response;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services;

public class HotelAdminService : IHotelAdminService
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;

    public HotelAdminService(
        IHotelRepository hotelRepository,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
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

        return _mapper.Map<IEnumerable<HotelAdminResponseDTO>>(hotels);
    }
}