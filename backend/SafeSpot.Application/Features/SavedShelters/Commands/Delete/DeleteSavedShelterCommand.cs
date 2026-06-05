using MediatR;

namespace SafeSpot.Application.Features.SavedShelters.Commands.Delete;

public record DeleteSavedShelterCommand(long UserId, long ShelterId) : IRequest;
