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

public class HotelReviewAdminService : IHotelReviewAdminService
{
    private readonly IHotelReviewRepository _hotelReviewRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<HotelReviewAdminService> _logger;
    private readonly ICurrentUserService _currentUserService;

    public HotelReviewAdminService(
        IHotelReviewRepository hotelReviewRepository,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper,
        ILogger<HotelReviewAdminService> logger,
        ICurrentUserService currentUserService)
    {
        _hotelReviewRepository = hotelReviewRepository;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
        _logger = logger;
        _currentUserService = currentUserService;
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

        _logger.LogInformation(
            "Searching for HotelReviews with query {@AdminReviewSearchQuery} by User {UserId}", 
            inQuery,
            _currentUserService.GetUserId());

        return _mapper.Map<IEnumerable<HotelReviewAdminResponseDTO>>(reviews);
    }
}