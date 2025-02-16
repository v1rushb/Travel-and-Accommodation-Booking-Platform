using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Enums;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Room.Search;
using TABP.API.Extensions;
using TABP.Domain.Models.Room.Sort;

namespace TABP.API.Controllers;

[Authorize(Roles = $"{nameof(RoleType.User)},{nameof(RoleType.Admin)}")]
[ApiController]
[Route("api/hotel-rooms")]
public class RoomController : ControllerBase
{
    private readonly IRoomUserService _roomUserService;

    public RoomController(
        IRoomUserService roomUserService)
    {
        _roomUserService = roomUserService;
    }

    [HttpGet("search")] 
    public async Task<IActionResult> SearchRoomsAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] RoomSearchQuery query,
        [FromQuery] RoomSortQuery sortQuery)
    {
        var rooms = await _roomUserService
            .SearchAsync(
                query,
                pagination,
                sortQuery
            );

        var roomCount = rooms.Count();

        Response.Headers
            .AddPaginationHeaders(
                roomCount,
                pagination
            );

        return Ok(rooms);
    }
}