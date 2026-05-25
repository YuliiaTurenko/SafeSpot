using MediatR;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Shelters.Queries.GetById;

public record GetShelterByIdQuery(long Id) : IRequest<ShelterDto>;