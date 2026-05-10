using MediatR;
using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.Features.Shelters.Commands.Create;
public record CreateShelterCommand(
    string Address,
    double Latitude,
    double Longitude,
    int Capacity,
    ShelterStatus Status,
    string? Description,
    string? ImageUrl
) : IRequest<long>;