using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Application.Filters.ExpressionBuilders;
using TABP.Application.Sorting.ExpressionBuilders;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Room.Search;
using TABP.Domain.Models.Room.Search.Response;
using TABP.Domain.Models.Room.Sort;

namespace TABP.Application.Services.Room;

public class RoomAdminService : IRoomAdminService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly ILogger<RoomAdminService> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly IValidator<RoomSortQuery> _roomSortQueryValidator;

    public RoomAdminService(
        IRoomRepository roomRepository,
        IValidator<PaginationDTO> paginationValidator,
        ILogger<RoomAdminService> logger,
        ICurrentUserService currentUserService,
        IMapper mapper,
        IValidator<RoomSortQuery> roomSortQueryValidator)
    {
        _roomRepository = roomRepository;
        _paginationValidator = paginationValidator;
        _logger = logger;
        _currentUserService = currentUserService;
        _mapper = mapper;
        _roomSortQueryValidator = roomSortQueryValidator;
    }

    public async Task<IEnumerable<RoomAdminResponseDTO>> SearchAsync(
        RoomSearchQuery query,
        PaginationDTO pagination,
        RoomSortQuery sortQuery)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        sortQuery.IsAdmin = true;
        _roomSortQueryValidator.ValidateAndThrow(sortQuery);


        var expression = RoomExpressionBuilder.Build(query);
        var orderBy = RoomSortExpressionBuilder
            .GetSortDelegate(sortQuery);

        _logger.LogInformation(
            @"Searching for Rooms with query {RoomSearchQuery},
            With Sort Query: {RoomSortQuery}, 
            PageNumber: {PageNumber}, 
            PageSize: {PageSize}
            By User {UserId}",

            query,
            sortQuery,
            pagination.PageNumber,
            pagination.PageSize,
            _currentUserService.GetUserId());

        var rooms = await _roomRepository.SearchAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize,
            orderBy
        );

        return _mapper
            .Map<IEnumerable<RoomAdminResponseDTO>>(rooms);
    }
}