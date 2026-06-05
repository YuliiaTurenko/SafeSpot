using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.Features.SavedShelters.Commands.Create;
using SafeSpot.Application.Features.SavedShelters.Commands.Delete;
using SafeSpot.Application.Features.SavedShelters.Queries.GetFavorite;
using SafeSpot.Application.Features.SavedShelters.Queries.GetIsSaved;

namespace SafeSpot.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/saved-shelters")]
public class SavedSheltersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepository;

    public SavedSheltersController(
        IMediator mediator,
        IUserContext userContext,
        IUserRepository userRepository)
    {
        _mediator = mediator;
        _userContext = userContext;
        _userRepository = userRepository;
    }

    [HttpPost("{shelterId}")]
    public async Task<IActionResult> Create(long shelterId)
    {
        var identityId = _userContext.GetApplicationUserId();
        long userId = await _userRepository.GetUserIdByIdentityIdAsync(identityId);

        var result = await _mediator.Send(
            new CreateSavedShelterCommand(
                userId,
                shelterId));

        return Ok(result);
    }

    [HttpDelete("{shelterId}")]
    public async Task<IActionResult> Delete(long shelterId)
    {
        var identityId = _userContext.GetApplicationUserId();
        long userId = await _userRepository.GetUserIdByIdentityIdAsync(identityId);

        await _mediator.Send(
            new DeleteSavedShelterCommand(
                userId,
                shelterId));

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetMySaved()
    {
        var identityId = _userContext.GetApplicationUserId();
        long userId = await _userRepository.GetUserIdByIdentityIdAsync(identityId);

        return Ok(await _mediator.Send(
            new GetFavoriteSheltersQuery(userId)));
    }

    [HttpGet("is-saved")]
    public async Task<IActionResult> IsSaved(long shelterId)
    {
        var identityId = _userContext.GetApplicationUserId();
        long userId = await _userRepository.GetUserIdByIdentityIdAsync(identityId);

        var result = await _mediator.Send(
            new IsShelterSavedQuery(
                userId,
                shelterId));

        return Ok(result);
    }
}