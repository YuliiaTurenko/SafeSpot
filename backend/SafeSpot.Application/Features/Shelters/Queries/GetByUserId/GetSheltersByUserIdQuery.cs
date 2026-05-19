using MediatR;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Shelters.Queries.GetByUserId;

public record GetSheltersByUserIdQuery(long userId) : IRequest<List<ShelterDto>>;