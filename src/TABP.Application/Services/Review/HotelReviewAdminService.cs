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

public class HotelReviewAdminService : IHotelReviewAdminService
{
    private readonly IHotelReviewRepository _hotelReviewRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<HotelReviewAdminService> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<ReviewSortQuery> _reviewSortQueryValidator;

    public HotelReviewAdminService(
        IHotelReviewRepository hotelReviewRepository,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper,
        ILogger<HotelReviewAdminService> logger,
        ICurrentUserService currentUserService,
        IValidator<ReviewSortQuery> reviewSortQueryValidator)
    {
        _hotelReviewRepository = hotelReviewRepository;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
        _logger = logger;
        _currentUserService = currentUserService;
        _reviewSortQueryValidator = reviewSortQueryValidator;
    }

    public async Task<IEnumerable<HotelReviewAdminResponseDTO>> SearchAsync(
        AdminReviewSearchQuery inQuery,
        PaginationDTO pagination,
        ReviewSortQuery sortQuery)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        sortQuery.IsAdmin = true;
        _reviewSortQueryValidator.ValidateAndThrow(sortQuery);

        var filterExpression = ReviewForAdminExpressionBuilder.Build(inQuery);
        var orderByDelegate = ReviewSortExpressionBuilder
            .GetSortDelegate(sortQuery);

        var reviews = await _hotelReviewRepository.SearchAsync(
            filterExpression,
            pagination.PageNumber,
            pagination.PageSize,
            orderByDelegate);

        _logger.LogInformation(
            @"
                Searching for HotelReviews with query {AdminReviewSearchQuery}
                Sorting: {ReviewSortQuery}
                PageNumber: {PageNumber}
                PageSize: {PageSize}
                By User With Id: {UserId}",

                inQuery,
                sortQuery,
                pagination.PageNumber,
                pagination.PageSize,
                _currentUserService.GetUserId());

        return _mapper.Map<IEnumerable<HotelReviewAdminResponseDTO>>(reviews);
    }
}