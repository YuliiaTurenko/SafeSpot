using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSpot.Api.Contracts.Announcements;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.Features.Announcements.Commands.Create;
using SafeSpot.Application.Features.Announcements.Commands.Delete;
using SafeSpot.Application.Features.Announcements.Commands.Update;
using SafeSpot.Application.Features.Announcements.Queries.GetAllByShelterId;

namespace SafeSpot.Api.Controllers;

[ApiController]
[Route("api/announcements")]
public class AnnouncementController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserRepository _userRepo;
    private readonly IUserContext _userContext;

    public AnnouncementController(IMediator mediator,
        IUserRepository userRepo, IUserContext userContext)
    {
        _mediator = mediator;
        _userRepo = userRepo;
        _userContext = userContext;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetByShelterId(long shelterId)
    {
        return Ok(await _mediator.Send(new GetAllAnnouncementsByShelterIdQuery(shelterId)));
    }

    [Authorize(Roles = "Admin, Operator")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAnnouncementRequest request)
    {
        var identityId = _userContext.GetApplicationUserId();
        long userId = await _userRepo.GetUserIdByIdentityIdAsync(identityId);

        var command = new CreateAnnouncementCommand(
            UserId: userId,
            ShelterId: request.ShelterId,
            Title: request.Title,
            Text: request.Text,
            ImageUrl: request.ImageUrl
        );

        return Ok(await _mediator.Send(command));
    }

    [Authorize(Roles = "Admin, Operator")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAnnouncementRequest request)
    {
        var identityId = _userContext.GetApplicationUserId();
        long userId = await _userRepo.GetUserIdByIdentityIdAsync(identityId);

        var command = new UpdateAnnouncementCommand(
           UserId: userId,
           Id: request.AnnouncementId,
           Title: request.Title,
           Text: request.Text,
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

        await _mediator.Send(new DeleteAnnouncementCommand(userId, id));
        return Ok();
    }
}