using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TABP.API.Extensions;
using TABP.Domain.Abstractions.Services;
using TABP.Domain.Models.Pagination;
using TABP.Domain.Models.Room;
using TABP.Domain.Models.Room.Search;
using TABP.Domain.Models.Room.Search.Response;

namespace TABP.API.Controllers.Room;

// [Authorize]
[ApiController]
[Route("api/admin/rooms")] 
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;
    private readonly IMapper _mapper;

    public RoomController(
        IRoomService roomService,
        IMapper mapper)
    {
        _roomService = roomService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoomAsync([FromBody] RoomForCreationDTO newRoom)
    {
        await _roomService.AddAsync(_mapper.Map<RoomDTO>(newRoom));

        return Created();
    }

    [HttpGet("{roomId:guid}")]
    public async Task<IActionResult> SearchByIdAsync(Guid roomId)
    {
        var room = await _roomService.GetByIdAsync(roomId);
        _mapper.Map<RoomForAdminWithoutIdDTO>(room);
        
        return Ok(room);
    }

    [HttpPatch("{roomId:guid}")]
    public async Task<IActionResult> PatchHotelAsync(
        Guid roomId,
        [FromBody] JsonPatchDocument<RoomForUpdateDTO> patchDoc)
    {
        var room = await _roomService.GetByIdAsync(roomId);
        var roomToPartiallyUpdate = GetRoomForPartialUpdate(patchDoc, room);

        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(roomToPartiallyUpdate, room);
        await _roomService.UpdateAsync(room);

        return NoContent();
    }

    private RoomForUpdateDTO GetRoomForPartialUpdate(
        JsonPatchDocument<RoomForUpdateDTO> patchDoc,
        RoomDTO city)
    {
        var roomToUpdate = _mapper.Map<RoomForUpdateDTO>(city);
        patchDoc.ApplyTo(roomToUpdate, ModelState);

        return roomToUpdate;
    }

    [HttpDelete("{roomId:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid roomId)
    {
        await _roomService.DeleteAsync(roomId);

        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchRoomsForAdminAsync(
        [FromQuery] PaginationDTO pagination,
        [FromQuery] RoomSearchQuery query)
    {
        var rooms = await _roomService.SearchAdminAsync(query, pagination);
        var roomsCount = rooms.Count();

        Response.Headers.AddPaginationHeaders(roomsCount, pagination);

        return Ok(rooms);
    }
}