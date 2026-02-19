using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MeetingRoom.Api.Models;
using MeetingRoom.Application.DTOs.Reservation;
using MeetingRoom.Application.Services;

namespace MeetingRoom.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;
    private readonly IValidator<CreateReservationDto> _createValidator;
    private readonly IValidator<UpdateReservationDto> _updateValidator;

    public ReservationsController(IReservationService reservationService, IValidator<CreateReservationDto> createValidator, IValidator<UpdateReservationDto> updateValidator)
    {
        _reservationService = reservationService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ReservationDto>>>> GetList(
        [FromQuery] int? roomId,
        [FromQuery] string? userName,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        CancellationToken cancellationToken)
    {
        var filter = new ReservationFilterDto { RoomId = roomId, UserName = userName, From = from, To = to };
        var list = await _reservationService.GetListAsync(filter, cancellationToken);
        return Ok(new ApiResponse<IReadOnlyList<ReservationDto>> { Success = true, Data = list });
    }

    [HttpGet("conflicts")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ReservationDto>>>> GetConflicts(
        [FromQuery] int roomId,
        [FromQuery] DateTime start,
        [FromQuery] DateTime end,
        [FromQuery] int? excludeReservationId,
        CancellationToken cancellationToken)
    {
        var list = await _reservationService.GetConflictsAsync(roomId, start, end, excludeReservationId, cancellationToken);
        return Ok(new ApiResponse<IReadOnlyList<ReservationDto>> { Success = true, Data = list });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<ReservationDto>>> GetById(int id, CancellationToken cancellationToken)
    {
        var reservation = await _reservationService.GetByIdAsync(id, cancellationToken);
        if (reservation is null)
            return NotFound(new ApiResponse<ReservationDto> { Success = false, Message = "Reservation not found." });
        return Ok(new ApiResponse<ReservationDto> { Success = true, Data = reservation });
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ReservationDto>>> Create([FromBody] CreateReservationDto dto, CancellationToken cancellationToken)
    {
        var result = await _createValidator.ValidateAsync(dto, cancellationToken);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        var reservation = await _reservationService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, new ApiResponse<ReservationDto> { Success = true, Data = reservation });
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ApiResponse<ReservationDto>>> Update(int id, [FromBody] UpdateReservationDto dto, CancellationToken cancellationToken)
    {
        var result = await _updateValidator.ValidateAsync(dto, cancellationToken);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
        var reservation = await _reservationService.UpdateAsync(id, dto, cancellationToken);
        if (reservation is null)
            return NotFound(new ApiResponse<ReservationDto> { Success = false, Message = "Reservation not found or canceled." });
        return Ok(new ApiResponse<ReservationDto> { Success = true, Data = reservation });
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ApiResponse<object>>> Cancel(int id, CancellationToken cancellationToken)
    {
        var canceled = await _reservationService.CancelAsync(id, cancellationToken);
        if (!canceled)
            return NotFound(new ApiResponse<object> { Success = false, Message = "Reservation not found or already canceled." });
        return Ok(new ApiResponse<object> { Success = true, Message = "Reservation canceled." });
    }
}
