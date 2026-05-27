using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSpot.Application.Features.Shelters.Queries.GetAnalytics;

namespace SafeSpot.Api.Controllers;

[ApiController]
[Route("api/sensor-analytics")]
[Authorize]
public class SensorAnalyticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SensorAnalyticsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(long shelterId)
    {
        return Ok(await _mediator.Send(
            new GetShelterAnalyticsQuery(shelterId)
        ));
    }
}