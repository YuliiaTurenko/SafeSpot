using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSpot.Api.Contracts.Shelters;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.Features.Shelters.Commands.Create;
using SafeSpot.Application.Features.Shelters.Commands.Delete;
using SafeSpot.Application.Features.Shelters.Commands.Update;
using SafeSpot.Application.Features.Shelters.Queries.GetAll;
using SafeSpot.Application.Features.Shelters.Queries.GetByUserId;

namespace SafeSpot.Api.Controllers;

[ApiController]
[Route("api/shelters")]
public class ShelterController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepo;
    private readonly IUserContext _userContext;

    public ShelterController(IMediator mediator,
        IUserRepository userRepo, IUserContext userContext)
    {
        _mediator = mediator;
        _userRepo = userRepo;
        _userContext = userContext;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllSheltersQuery()));
    }

    [Authorize]
    [HttpGet("user-{id}")]
    public async Task<IActionResult> GetAllForUser()
    {
        var identityId = _userContext.GetApplicationUserId();
        long userId = await _userRepo.GetUserIdByIdentityIdAsync(identityId);
        var result = await _mediator.Send(new GetSheltersByUserIdQuery(userId));
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateShelterRequest request)
    {
        var identityId = _userContext.GetApplicationUserId();
        long userId = await _userRepo.GetUserIdByIdentityIdAsync(identityId);

        var command = new CreateShelterCommand(
            UserId: userId,
            Address: request.Address,
            Latitude: request.Latitude,
            Longitude: request.Longitude,
            Capacity: request.Capacity,
            Status: request.Status,
            Description: request.Description,
            ImageUrl: request.ImageUrl
        );

        return Ok(await _mediator.Send(command));
    }

    [Authorize(Roles = "Admin, Operator")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateShelterRequest request)
    {
        var identityId = _userContext.GetApplicationUserId();
        long userId = await _userRepo.GetUserIdByIdentityIdAsync(identityId);

        var command = new UpdateShelterCommand(
           UserId: userId,
           Id: request.Id,
           Address: request.Address,
           Latitude: request.Latitude,
           Longitude: request.Longitude,
           Capacity: request.Capacity,
           Status: request.Status,
           Description: request.Description,
           ImageUrl: request.ImageUrl
        );

        await _mediator.Send(command);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var identityId = _userContext.GetApplicationUserId();
        long userId = await _userRepo.GetUserIdByIdentityIdAsync(identityId);

        await _mediator.Send(new DeleteShelterCommand(userId, id));
        return Ok();
    }
}