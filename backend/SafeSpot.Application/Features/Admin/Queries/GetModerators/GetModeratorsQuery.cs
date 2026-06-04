using MediatR;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Admin.Queries.GetModerators;

public record GetModeratorsQuery(long AdminUserId) : IRequest<List<ModeratorListItemDto>>;
