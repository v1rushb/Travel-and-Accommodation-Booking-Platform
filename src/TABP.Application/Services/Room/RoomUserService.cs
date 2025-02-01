using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Room.Search;
using TABP.Domain.Models.Room.Search.Response;

namespace TABP.Application.Services.Room;

public class RoomUserService : IRoomUserService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<RoomUserService> _logger;
    private readonly ICurrentUserService _currentUserService;

    public RoomUserService(
        IRoomRepository roomRepository,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper,
        ILogger<RoomUserService> logger,
        ICurrentUserService currentUserService)
    {
        _roomRepository = roomRepository;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<RoomUserResponseDTO>> SearchAsync(
        RoomSearchQuery query,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var expression = RoomExpressionBuilder.Build(query);
        var rooms = await _roomRepository.SearchAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);

        _logger.LogInformation(
            "Searching for rooms with query {@RoomSearchQuery} by User {UserId}",
            query,
            _currentUserService.GetUserId());


        return _mapper.Map<IEnumerable<RoomUserResponseDTO>>(rooms);
    }
}