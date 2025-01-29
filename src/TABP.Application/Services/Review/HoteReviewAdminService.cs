using AutoMapper;
using FluentValidation;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories.Review;
using TABP.Domain.Models.HotelReview.Search;
using TABP.Domain.Models.HotelReview.Search.Response;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services.Review;

public class HotelReviewAdminService : IHotelReviewAdminService
{
    private readonly IHotelReviewRepository _hotelReviewRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;

    public HotelReviewAdminService(
        IHotelReviewRepository hotelReviewRepository,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper)
    {
        _hotelReviewRepository = hotelReviewRepository;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
    }

    public async Task<IEnumerable<HotelReviewAdminResponseDTO>> SearchAsync(
        AdminReviewSearchQuery inQuery,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var expression = ReviewForAdminExpressionBuilder.Build(inQuery);
        var reviews = await _hotelReviewRepository.SearchAsync( 
            expression,
            pagination.PageNumber,
            pagination.PageSize);
        
        return _mapper.Map<IEnumerable<HotelReviewAdminResponseDTO>>(reviews);
    }
}