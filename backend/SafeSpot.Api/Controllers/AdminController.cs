using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SafeSpot.Application.DTOs;
using SafeSpot.Infrastructure.Services;

namespace SafeSpot.Api.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly AdminService _service;

    public AdminController(AdminService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpPost("request")]
    public async Task<IActionResult> RequestAdmin(AdminRequestDto dto)
    {
        await _service.SendAdminRequestAsync(dto.Message);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("assign")]
    public async Task<IActionResult> AssignAdmin(AssignAdminDto dto)
    {
        await _service.AssignAdminAsync(dto.Email);
        return Ok();
    }
}