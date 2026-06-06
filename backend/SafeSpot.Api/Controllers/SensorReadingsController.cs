using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSpot.Api.Contracts.SensorReadings;
using SafeSpot.Application.Features.SensorReadings.Commands.Create;
using SafeSpot.Application.Features.SensorReadings.Queries.GetBySensorId;
using SafeSpot.Application.Features.SensorReadings.Queries.GetMonthly;
using SafeSpot.Application.Features.SensorReadings.Queries.GetWeekly;

namespace SafeSpot.Api.Controllers;

[ApiController]
[Route("api/sensor-readings")]
public class SensorReadingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SensorReadingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{sensorId}")]
    public async Task<IActionResult> GetLatestBySensorId(long sensorId)
    {
        return Ok(await _mediator.Send(
                new GetSensorReadingsQuery(sensorId)));
    }

    [HttpGet("{sensorId}/weekly")]
    public async Task<IActionResult> GetWeeklyBySensorId(long sensorId)
    {
        return Ok(await _mediator.Send(
                new GetWeeklySensorReadingsQuery(sensorId)));
    }

    [HttpGet("{sensorId}/monthly")]
    public async Task<IActionResult> GetMonthlyBySensorId(long sensorId)
    {
        return Ok(await _mediator.Send(
                new GetMonthlySensorReadingsQuery(sensorId)));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSensorReadingRequest request)
    {
        return Ok(await _mediator.Send(
                new CreateSensorReadingCommand(request.SensorId, request.Value)));
    }
}