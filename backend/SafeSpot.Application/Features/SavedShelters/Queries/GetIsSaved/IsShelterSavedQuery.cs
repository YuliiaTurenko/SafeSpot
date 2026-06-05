using MediatR;

namespace SafeSpot.Application.Features.SavedShelters.Queries.GetIsSaved;

public record IsShelterSavedQuery(long UserId, long ShelterId) : IRequest<bool>;