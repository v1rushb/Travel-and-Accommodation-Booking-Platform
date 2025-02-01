using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories.Review;
using TABP.Domain.Abstractions.Services.Review;
using TABP.Domain.Models.HotelReview.Search;
using TABP.Domain.Models.HotelReview.Search.Response;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services.Review;

public class HotelReviewUserService : IHotelReviewUserService
{
    private readonly IHotelReviewRepository _hotelReviewRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<HotelReviewUserService> _logger;

    public HotelReviewUserService(
        IHotelReviewRepository hotelReviewRepository,
        ICurrentUserService currentUserService,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper,
        ILogger<HotelReviewUserService> logger)
    {
        _hotelReviewRepository = hotelReviewRepository;
        _currentUserService = currentUserService;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
        _logger = logger;
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

        _logger.LogInformation(
            "Searching for HotelReviews with query {@ReviewSearchQuery} by User {UserId}", 
            query, 
            _currentUserService.GetUserId());

        return _mapper.Map<IEnumerable<HotelReviewUserResponseDTO>>(reviews);
    }

}