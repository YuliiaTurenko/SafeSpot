using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSpot.Application.Features.User.Commands.Create;
using SafeSpot.Application.Features.User.Commands.Update;
using SafeSpot.Application.Features.User.Queries.GetById;

namespace SafeSpot.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> Update(UpdateUserCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet("me")]
    public async Task<IActionResult> Get(long id)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(id));
        return Ok(result);
    }
}