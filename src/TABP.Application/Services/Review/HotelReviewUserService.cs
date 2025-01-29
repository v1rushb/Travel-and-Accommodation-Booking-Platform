using AutoMapper;
using FluentValidation;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories.Review;
using TABP.Domain.Models.HotelReview.Search;
using TABP.Domain.Models.HotelReview.Search.Response;
using TABP.Domain.Models.Pagination;

namespace TABP.Domain.Abstractions.Services.Review;

public class HotelReviewUserService : IHotelReviewUserService
{
    private readonly IHotelReviewRepository _hotelReviewRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;

    public HotelReviewUserService(
        IHotelReviewRepository hotelReviewRepository,
        ICurrentUserService currentUserService,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper)
    {
        _hotelReviewRepository = hotelReviewRepository;
        _currentUserService = currentUserService;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
    }
    public async Task<IEnumerable<HotelReviewUserResponseDTO>> SearchAsync(
        ReviewSearchQuery query,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var currentUserId = _currentUserService.GetUserId();
        
        var expression = ReviewExpressionBuilder.Build(query, currentUserId);

        var reviews = await _hotelReviewRepository.SearchAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);

        return _mapper.Map<IEnumerable<HotelReviewUserResponseDTO>>(reviews);
    }

}