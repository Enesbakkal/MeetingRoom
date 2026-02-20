using MediatR;
using Microsoft.AspNetCore.Mvc;
using MeetingRoom.Api.Models;
using MeetingRoom.Application.DTOs.ReservationSeries;
using MeetingRoom.Application.Series.Commands;
using MeetingRoom.Application.Series.Queries;

namespace MeetingRoom.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationSeriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservationSeriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ReservationSeriesDto>>>> List(CancellationToken cancellationToken)
    {
        var list = await _mediator.Send(new ListReservationSeriesQuery(), cancellationToken);
        return Ok(new ApiResponse<IReadOnlyList<ReservationSeriesDto>> { Success = true, Data = list });
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ApiResponse<ReservationSeriesDto>>> GetById(int id, CancellationToken cancellationToken)
    {
        var series = await _mediator.Send(new GetReservationSeriesQuery(id), cancellationToken);
        if (series is null)
            return NotFound(new ApiResponse<ReservationSeriesDto> { Success = false, Message = "Reservation series not found." });
        return Ok(new ApiResponse<ReservationSeriesDto> { Success = true, Data = series });
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ReservationSeriesDto>>> Create([FromBody] CreateReservationSeriesRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateReservationSeriesCommand(request.Name, request.Pattern, request.StartDate, request.EndDate);
        var series = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = series.Id }, new ApiResponse<ReservationSeriesDto> { Success = true, Data = series });
    }

    [HttpGet("{id:int}/exceptions")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ReservationExceptionDto>>>> GetExceptions(int id, CancellationToken cancellationToken)
    {
        var list = await _mediator.Send(new GetReservationExceptionsQuery(id), cancellationToken);
        return Ok(new ApiResponse<IReadOnlyList<ReservationExceptionDto>> { Success = true, Data = list });
    }

    [HttpPost("{id:int}/exceptions")]
    public async Task<ActionResult<ApiResponse<ReservationExceptionDto>>> AddException(int id, [FromBody] AddReservationExceptionRequest request, CancellationToken cancellationToken)
    {
        var command = new AddReservationExceptionCommand(id, request.ExceptionDate);
        var exception = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetExceptions), new { id }, new ApiResponse<ReservationExceptionDto> { Success = true, Data = exception });
    }
}
