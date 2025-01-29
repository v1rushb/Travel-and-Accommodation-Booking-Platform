using AutoMapper;
using FluentValidation;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Room.Search;
using TABP.Domain.Models.Room.Search.Response;

namespace TABP.Domain.Abstractions.Services.Room;

public class RoomUserService : IRoomUserService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;
    private readonly IMapper _mapper;

    public RoomUserService(
        IRoomRepository roomRepository,
        IValidator<PaginationDTO> paginationValidator,
        IMapper mapper)
    {
        _roomRepository = roomRepository;
        _paginationValidator = paginationValidator;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoomUserResponseDTO>> SearchAsync(
        RoomSearchQuery query,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var expression = RoomExpressionBuilder.Build(query);
        var rooms = await _roomRepository.SearchAdminAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);

        return _mapper.Map<IEnumerable<RoomUserResponseDTO>>(rooms);
    }   
}