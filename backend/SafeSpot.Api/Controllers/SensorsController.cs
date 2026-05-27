using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.Features.Sensors.Commands.Create;
using SafeSpot.Application.Features.Sensors.Commands.Delete;
using SafeSpot.Application.Features.Sensors.Commands.Update;
using SafeSpot.Application.Features.Sensors.Queries.GetByShelterId;
using SafeSpot.Api.Contracts.Sensors;

namespace SafeSpot.Api.Controllers;

[ApiController]
[Route("api/sensors")]
public class SensorsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepo;

    public SensorsController(
        IMediator mediator,
        IUserContext userContext,
        IUserRepository userRepo)
    {
        _mediator = mediator;
        _userContext = userContext;
        _userRepo = userRepo;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetByShelterId(long shelterId)
    {
        return Ok(await _mediator.Send(
                new GetSensorsByShelterIdQuery(shelterId)));
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(CreateSensorRequest request)
    {
        var identityId = _userContext.GetApplicationUserId();

        long userId = await _userRepo.GetUserIdByIdentityIdAsync(identityId);

        return Ok(await _mediator
            .Send(
                new CreateSensorCommand(
                    userId,
                    request.ShelterId,
                    request.Type,
                    request.MinValue,
                    request.MaxValue
                )));
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update(UpdateSensorRequest request)
    {
        var identityId = _userContext.GetApplicationUserId();

        long userId = await _userRepo.GetUserIdByIdentityIdAsync(identityId);

        await _mediator.Send(
            new UpdateSensorCommand(
                userId,
                request.SensorId,
                request.Status,
                request.MinValue,
                request.MaxValue
            ));

        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var identityId = _userContext.GetApplicationUserId();

        long userId = await _userRepo.GetUserIdByIdentityIdAsync(identityId);

        await _mediator.Send(new DeleteSensorCommand(userId, id));

        return Ok();
    }
}