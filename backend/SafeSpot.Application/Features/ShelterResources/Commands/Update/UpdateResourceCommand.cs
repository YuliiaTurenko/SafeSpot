using MediatR;
using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.Features.ShelterResources.Commands.Update;

public record UpdateResourceCommand(
    long Id,
    ResourceStatus Status,
    int Amount
) : IRequest;