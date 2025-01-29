using FluentValidation;
using TABP.Domain.Abstractions.Repositories;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Room.Search;
using TABP.Domain.Models.Room.Search.Response;

namespace TABP.Domain.Abstractions.Services.Room;

public class RoomAdminService : IRoomAdminService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IValidator<PaginationDTO> _paginationValidator;

    public RoomAdminService(
        IRoomRepository roomRepository,
        IValidator<PaginationDTO> paginationValidator)
    {
        _roomRepository = roomRepository;
        _paginationValidator = paginationValidator;
    }
    
    public async Task<IEnumerable<RoomAdminResponseDTO>> SearchAsync(
        RoomSearchQuery query,
        PaginationDTO pagination)
    {
        _paginationValidator.ValidateAndThrow(pagination);

        var expression = RoomExpressionBuilder.Build(query);
        return await _roomRepository.SearchAdminAsync(
            expression,
            pagination.PageNumber,
            pagination.PageSize);
    }
}