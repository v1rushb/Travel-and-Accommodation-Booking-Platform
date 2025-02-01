using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Room.Search;
using TABP.Domain.Models.Room.Search.Response;

namespace TABP.Application.Services.Room;

public class RoomAdminService : IRoomAdminService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly ILogger<RoomAdminService> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public RoomAdminService(
        IRoomRepository roomRepository,
        IValidator<PaginationDTO> paginationValidator,
        ILogger<RoomAdminService> logger,
        ICurrentUserService currentUserService,
        IMapper mapper)
    {
        _roomRepository = roomRepository;
        _paginationValidator = paginationValidator;
        _logger = logger;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoomAdminResponseDTO>> SearchAsync(
        RoomSearchQuery query,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var expression = RoomExpressionBuilder.Build(query);

        _logger.LogInformation(
            "Searching for Rooms with query {@RoomSearchQuery} by User {UserId}",
            query,
            _currentUserService.GetUserId());

        var rooms = await _roomRepository.SearchAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);

        return _mapper
            .Map<IEnumerable<RoomAdminResponseDTO>>(rooms);
    }
}