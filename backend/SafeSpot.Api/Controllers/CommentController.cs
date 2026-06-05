using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSpot.Api.Contracts.Comments;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.Features.Comments.Commands.Create;
using SafeSpot.Application.Features.Comments.Commands.Delete;
using SafeSpot.Application.Features.Comments.Commands.Update;
using SafeSpot.Application.Features.Comments.Queries.GetByPostId;

namespace SafeSpot.Api.Controllers;

[ApiController]
[Route("api/comments")]
public class CommentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepo;

    public CommentController(IMediator mediator, IUserContext userContext, IUserRepository userRepo)
    {
        _mediator = mediator;
        _userContext = userContext;
        _userRepo = userRepo;
    }

    [Authorize]
    [HttpGet("post-{postId}")]
    public async Task<IActionResult> GetByPostId(long postId)
    {
        var identityId = _userContext.GetApplicationUserId();
        var result = await _mediator.Send(new GetCommentsByPostIdQuery(postId, identityId));
        return Ok(result);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentRequest request)
    {
        var identityId = _userContext.GetApplicationUserId();

        var command = new CreateCommentCommand(
            IdentityId: identityId,
            PostId: request.PostId,
            Text: request.Text
        );

        var commentId = await _mediator.Send(command);
        return Ok(commentId);
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCommentRequest request)
    {
        var identityId = _userContext.GetApplicationUserId();
        long userId = await _userRepo.GetUserIdByIdentityIdAsync(identityId);

        var command = new UpdateCommentCommand(
            UserId: userId,
            CommentId: request.CommentId,
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

        await _mediator.Send(new DeleteCommentCommand(userId, id));
        return Ok();
    }
}
