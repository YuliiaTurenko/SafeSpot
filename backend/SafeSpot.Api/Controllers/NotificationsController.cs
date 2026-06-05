using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.Features.Notifications.Commands.MarkAsRead;

namespace SafeSpot.Api.Controllers;

[ApiController]
[Route("api/notifications")]
[Authorize]
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly INotificationRepository _repo;
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepo;

    public NotificationsController(
        IMediator mediator,
        INotificationRepository repo,
        IUserContext userContext,
        IUserRepository userRepo)
    {
        _mediator = mediator;
        _repo = repo;
        _userContext = userContext;
        _userRepo = userRepo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var identityId = _userContext.GetApplicationUserId();

        var userId = await _userRepo.GetUserIdByIdentityIdAsync(identityId);

        return Ok(await _repo.GetByUserIdAsync(userId));
    }

    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnread()
    {
        var identityId = _userContext.GetApplicationUserId();
        var userId = await _userRepo.GetUserIdByIdentityIdAsync(identityId);

        return Ok(await _repo.GetUnreadCountAsync(userId));
    }

    [HttpPut("{id}/read")]
    public async Task<IActionResult> MarkAsRead(long id)
    {
        await _mediator.Send(new MarkNotificationAsReadCommand(id));

        return Ok();
    }
}
