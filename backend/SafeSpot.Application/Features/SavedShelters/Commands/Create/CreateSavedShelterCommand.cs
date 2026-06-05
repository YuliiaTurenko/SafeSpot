using MediatR;

namespace SafeSpot.Application.Features.SavedShelters.Commands.Create;

public record CreateSavedShelterCommand(long UserId, long ShelterId) : IRequest<long>;