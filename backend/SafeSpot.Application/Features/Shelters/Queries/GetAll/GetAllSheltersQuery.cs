using MediatR;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Shelters.Queries.GetAll;

public record GetAllSheltersQuery : IRequest<List<ShelterDto>>;