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

public class RoomUserService : IRoomUserService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<RoomUserService> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<RoomSortQuery> _roomSortQueryValidator;

    public RoomUserService(
        IRoomRepository roomRepository,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper,
        ILogger<RoomUserService> logger,
        ICurrentUserService currentUserService,
        IValidator<RoomSortQuery> roomSortQueryValidator)
    {
        _roomRepository = roomRepository;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
        _logger = logger;
        _currentUserService = currentUserService;
        _roomSortQueryValidator = roomSortQueryValidator;
    }

    public async Task<IEnumerable<RoomUserResponseDTO>> SearchAsync(
        RoomSearchQuery query,
        PaginationDTO pagination,
        RoomSortQuery sortQuery)
    {
        _paginationValidator.ValidateAndThrow(pagination);
        _roomSortQueryValidator.ValidateAndThrow(sortQuery);
        

        var expression = RoomExpressionBuilder.Build(query);
        var orderBy = RoomSortExpressionBuilder
            .GetSortDelegate(sortQuery);

        var rooms = await _roomRepository.SearchAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize,
            orderBy
        );

        _logger.LogInformation(
            @"Searching for Rooms with query {RoomSearchQuery},
            {RoomSortQuery}, 
            PageNumber: {PageNumber}, 
            PageSize: {PageSize}
            By User {UserId}",

            query,
            sortQuery,
            pagination.PageNumber,
            pagination.PageSize,
            _currentUserService.GetUserId());


        return _mapper.Map<IEnumerable<RoomUserResponseDTO>>(rooms);
    }
}