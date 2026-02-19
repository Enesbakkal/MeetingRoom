using Microsoft.AspNetCore.Mvc;
using MeetingRoom.Api.Models;
using MeetingRoom.Application.DTOs.Room;
using MeetingRoom.Application.Services;

namespace MeetingRoom.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<RoomDto>>>> GetAll(CancellationToken cancellationToken)
    {
        var list = await _roomService.GetAllAsync(cancellationToken);
        return Ok(new ApiResponse<IReadOnlyList<RoomDto>> { Success = true, Data = list });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<RoomDto>>> GetById(int id, CancellationToken cancellationToken)
    {
        var room = await _roomService.GetByIdAsync(id, cancellationToken);
        if (room is null)
            return NotFound(new ApiResponse<RoomDto> { Success = false, Message = "Room not found." });
        return Ok(new ApiResponse<RoomDto> { Success = true, Data = room });
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<RoomDto>>> Create([FromBody] CreateRoomDto dto, CancellationToken cancellationToken)
    {
        var room = await _roomService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, new ApiResponse<RoomDto> { Success = true, Data = room });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<RoomDto>>> Update(int id, [FromBody] UpdateRoomDto dto, CancellationToken cancellationToken)
    {
        var room = await _roomService.UpdateAsync(id, dto, cancellationToken);
        if (room is null)
            return NotFound(new ApiResponse<RoomDto> { Success = false, Message = "Room not found." });
        return Ok(new ApiResponse<RoomDto> { Success = true, Data = room });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _roomService.DeleteAsync(id, cancellationToken);
        if (!deleted)
            return NotFound(new ApiResponse<object> { Success = false, Message = "Room not found." });
        return Ok(new ApiResponse<object> { Success = true, Message = "Room deleted." });
    }
}
