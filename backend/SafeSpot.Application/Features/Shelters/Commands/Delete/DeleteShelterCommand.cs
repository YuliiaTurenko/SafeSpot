using MediatR;

namespace SafeSpot.Application.Features.Shelters.Commands.Delete;

public record DeleteShelterCommand(long UserId, long Id) : IRequest;
