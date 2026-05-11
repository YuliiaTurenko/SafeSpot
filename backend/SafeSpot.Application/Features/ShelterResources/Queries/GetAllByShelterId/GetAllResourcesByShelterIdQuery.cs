using MediatR;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.ShelterResources.Queries.GetAllByShelterId;

public record GetAllResourcesByShelterIdQuery(long ShelterId) : IRequest<List<ShelterResourceDto>>;