using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Application.Sorting.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories.Review;
using TABP.Domain.Abstractions.Services.Review;
using TABP.Domain.Models.HotelReview.Search;
using TABP.Domain.Models.HotelReview.Search.Response;
using TABP.Domain.Models.HotelReview.Sort;
using TABP.Domain.Models.Pagination;

namespace TABP.Application.Services.Review;

public class HotelReviewUserService : IHotelReviewUserService
{
    private readonly IHotelReviewRepository _hotelReviewRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<HotelReviewUserService> _logger;
    private readonly IValidator<ReviewSortQuery> _reviewSortQueryValidator;

    public HotelReviewUserService(
        IHotelReviewRepository hotelReviewRepository,
        ICurrentUserService currentUserService,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper,
        ILogger<HotelReviewUserService> logger,
        IValidator<ReviewSortQuery> reviewSortQueryValidator)
    {
        _hotelReviewRepository = hotelReviewRepository;
        _currentUserService = currentUserService;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
        _logger = logger;
        _reviewSortQueryValidator = reviewSortQueryValidator;
    }
    public async Task<IEnumerable<HotelReviewUserResponseDTO>> SearchAsync(
        ReviewSearchQuery query,
        PaginationDTO pagination,
        ReviewSortQuery sortQuery)
    {
        _paginationValidator.ValidateAndThrow(pagination);
        _reviewSortQueryValidator.ValidateAndThrow(sortQuery);

        var currentUserId = _currentUserService.GetUserId();

        var filterExpression = ReviewExpressionBuilder.Build(query, currentUserId);
        var orderByDelegate = ReviewSortExpressionBuilder
            .GetSortDelegate(sortQuery);

        var reviews = await _hotelReviewRepository.SearchAsync(
            filterExpression,
            pagination.PageNumber,
            pagination.PageSize,
            orderByDelegate
        );

        _logger.LogInformation(
            @"
                Searching for HotelReviews with query {ReviewSearchQuery}
                Sorting: {ReviewSortQuery}
                PageNumber: {PageNumber}
                PageSize: {PageSize}
                By User With Id: {UserId}",

                query,
                sortQuery,
                pagination.PageNumber,
                pagination.PageSize,
                _currentUserService.GetUserId()
            );

        return _mapper
            .Map<IEnumerable<HotelReviewUserResponseDTO>>(reviews);
    }

}