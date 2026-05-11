using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSpot.Application.Features.ShelterResources.Commands.Create;
using SafeSpot.Application.Features.ShelterResources.Commands.Delete;
using SafeSpot.Application.Features.ShelterResources.Commands.Update;
using SafeSpot.Application.Features.ShelterResources.Queries.GetAllByShelterId;

namespace SafeSpot.Api.Controllers;

[ApiController]
[Route("api/shelters-resources")]
public class ShelterResourceController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShelterResourceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll(long shelterId)
    {
        return Ok(await _mediator.Send(new GetAllResourcesByShelterIdQuery(shelterId)));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateResourceCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [Authorize(Roles = "Admin, Operator")]
    [HttpPut]
    public async Task<IActionResult> Update(UpdateResourceCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _mediator.Send(new DeleteResourceCommand(id));
        return Ok();
    }
}