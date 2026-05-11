using MediatR;
using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.Features.ShelterResources.Commands.Create;

public record CreateResourceCommand(
    long ShelterId,
    ResourceType Type,
    ResourceStatus Status,
    int Amount
) : IRequest<long>;