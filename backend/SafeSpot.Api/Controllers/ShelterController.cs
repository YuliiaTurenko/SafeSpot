using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSpot.Application.Features.Shelters.Commands.Create;
using SafeSpot.Application.Features.Shelters.Commands.Delete;
using SafeSpot.Application.Features.Shelters.Commands.Update;
using SafeSpot.Application.Features.Shelters.Queries.GetAll;

namespace SafeSpot.Api.Controllers;

[ApiController]
[Route("api/shelters")]
public class ShelterController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShelterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllSheltersQuery()));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateShelterCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [Authorize(Roles = "Admin, Operator")]
    [HttpPut]
    public async Task<IActionResult> Update(UpdateShelterCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _mediator.Send(new DeleteShelterCommand(id));
        return Ok();
    }
}