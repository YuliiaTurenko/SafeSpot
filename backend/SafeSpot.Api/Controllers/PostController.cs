using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSpot.Api.Contracts.Posts;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.Features.Posts.Commands.Create;
using SafeSpot.Application.Features.Posts.Commands.Delete;
using SafeSpot.Application.Features.Posts.Commands.Update;
using SafeSpot.Application.Features.Posts.Queries.GetByShelterId;

namespace SafeSpot.Api.Controllers;

[ApiController]
[Route("api/posts")]
public class PostController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepo;

    public PostController(IMediator mediator, IUserContext userContext, IUserRepository userRepo)
    {
        _mediator = mediator;
        _userContext = userContext;
        _userRepo = userRepo;
    }

    [Authorize]
    [HttpGet("shelter-{shelterId}")]
    public async Task<IActionResult> GetByShelterId(long shelterId)
    {
        var identityId = _userContext.GetApplicationUserId();
        var result = await _mediator.Send(new GetPostsByShelterIdQuery(shelterId, identityId));
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
    {
        var identityId = _userContext.GetApplicationUserId();

        var command = new CreatePostCommand(
            IdentityId: identityId,
            ShelterId: request.ShelterId,
            Text: request.Text
        );

        var postId = await _mediator.Send(command);
        return Ok(postId);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePostRequest request)
    {
        var identityId = _userContext.GetApplicationUserId();
        long userId = await _userRepo.GetUserIdByIdentityIdAsync(identityId);

        var command = new UpdatePostCommand(
            UserId: userId,
            PostId: request.PostId,
            Text: request.Text
        );

        await _mediator.Send(command);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var identityId = _userContext.GetApplicationUserId();
        long userId = await _userRepo.GetUserIdByIdentityIdAsync(identityId);

        await _mediator.Send(new DeletePostCommand(userId, id));
        return Ok();
    }
}
