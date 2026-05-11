using MediatR;

namespace SafeSpot.Application.Features.ShelterResources.Commands.Delete;

public record DeleteResourceCommand(long Id) : IRequest;