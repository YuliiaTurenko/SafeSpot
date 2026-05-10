using MediatR;
using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.Features.Shelters.Commands.Update;

public record UpdateShelterCommand(
    long Id,
    string Address,
    double Latitude,
    double Longitude,
    int Capacity,
    ShelterStatus Status,
    string? Description,
    string? ImageUrl
) : IRequest;
