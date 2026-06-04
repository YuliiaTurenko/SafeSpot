using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSpot.Api.Contracts.Admin;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;
using SafeSpot.Application.Features.Admin.Commands.AssignModerator;
using SafeSpot.Application.Features.Admin.Commands.RevokeModerator;
using SafeSpot.Application.Features.Admin.Queries.GetModerators;
using SafeSpot.Application.Features.Admin.Queries.GetUsersWithUserRole;
using SafeSpot.Infrastructure.Services;

namespace SafeSpot.Api.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly AdminService _service;
    private readonly IMediator _mediator;
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepo;

    public AdminController(
        AdminService service,
        IMediator mediator,
        IUserContext userContext,
        IUserRepository userRepo)
    {
        _service = service;
        _mediator = mediator;
        _userContext = userContext;
        _userRepo = userRepo;
    }

    [Authorize]
    [HttpPost("request")]
    public async Task<IActionResult> RequestAdmin(AdminRequestDto dto)
    {
        await _service.SendAdminRequestAsync(dto.Message);
        return Ok();
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignAdmin(AssignAdminDto dto)
    {
        await _service.AssignAdminAsync(dto.Email);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("moderators/assign")]
    public async Task<IActionResult> AssignModerator([FromBody] AssignModeratorRequest request)
    {
        var adminUserId = await GetCurrentUserIdAsync();

        await _mediator.Send(new AssignModeratorCommand(
            AdminUserId: adminUserId,
            TargetUserId: request.TargetUserId,
            ShelterId: request.ShelterId));

        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("moderators/revoke")]
    public async Task<IActionResult> RevokeModerator([FromBody] RevokeModeratorRequest request)
    {
        var adminUserId = await GetCurrentUserIdAsync();

        await _mediator.Send(new RevokeModeratorCommand(
            AdminUserId: adminUserId,
            TargetUserId: request.TargetUserId));

        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _mediator.Send(new GetUsersWithUserRoleQuery());
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("moderators")]
    public async Task<IActionResult> GetModerators()
    {
        var adminUserId = await GetCurrentUserIdAsync();
        var result = await _mediator.Send(new GetModeratorsQuery(adminUserId));
        return Ok(result);
    }

    private async Task<long> GetCurrentUserIdAsync()
    {
        var identityId = _userContext.GetApplicationUserId();
        return await _userRepo.GetUserIdByIdentityIdAsync(identityId!);
    }
}
