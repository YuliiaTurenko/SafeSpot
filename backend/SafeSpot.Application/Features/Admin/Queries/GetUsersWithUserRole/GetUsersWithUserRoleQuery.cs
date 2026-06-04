using MediatR;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Admin.Queries.GetUsersWithUserRole;

public record GetUsersWithUserRoleQuery : IRequest<List<AdminUserListItemDto>>;
