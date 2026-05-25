using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSpot.Application.Features.SensorReadings.Commands.Create;
using SafeSpot.Application.Features.SensorReadings.Queries.GetBySensorId;
using SafeSpot.Api.Contracts.SensorReadings;

namespace SafeSpot.Api.Controllers;

[ApiController]
[Route("api/sensor-readings")]
public class SensorReadingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SensorReadingsController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetBySensorId(long sensorId)
    {
        return Ok(await _mediator.Send(
                new GetSensorReadingsQuery(sensorId)));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSensorReadingRequest request)
    {
        return Ok(await _mediator.Send(
                new CreateSensorReadingCommand(request.SensorId, request.Value)));
    }
}