using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSpot.Api.Contracts.Sensors;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.Features.Sensors.Commands.Create;
using SafeSpot.Application.Features.Sensors.Commands.Delete;
using SafeSpot.Application.Features.Sensors.Commands.Update;
using SafeSpot.Application.Features.Sensors.Queries.GetById;
using SafeSpot.Application.Features.Sensors.Queries.GetByShelterId;

namespace SafeSpot.Api.Controllers;

[ApiController]
[Route("api/sensors")]
public class SensorsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepo;
    private readonly ISensorCommandPublisher _commandPublisher;

    public SensorsController(
        IMediator mediator,
        IUserContext userContext,
        IUserRepository userRepo,
        ISensorCommandPublisher commandPublisher)
    {
        _mediator = mediator;
        _userContext = userContext;
        _userRepo = userRepo;
        _commandPublisher = commandPublisher;
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

    [Authorize]
    [HttpPost("{id}/disable")]
    public async Task<IActionResult> Disable(long id)
    {
        var identityId = _userContext.GetApplicationUserId();

        long userId = await _userRepo.GetUserIdByIdentityIdAsync(identityId);

        var sensor = await _mediator.Send(new GetSensorByIdQuery(id, userId));

        await _commandPublisher.PublishDisableAsync(
            sensor.ShelterId,
            sensor.Id);

        return Ok();
    }

    [Authorize]
    [HttpPost("{id}/enable")]
    public async Task<IActionResult> Enable(long id)
    {
        var identityId = _userContext.GetApplicationUserId();

        long userId = await _userRepo.GetUserIdByIdentityIdAsync(identityId);

        var sensor = await _mediator.Send(new GetSensorByIdQuery(id, userId));

        await _commandPublisher.PublishEnableAsync(
            sensor.ShelterId,
            sensor.Id);

        return Ok();
    }
}