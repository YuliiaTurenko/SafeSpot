using MediatR;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.SavedShelters.Queries.GetFavorite;

public record GetFavoriteSheltersQuery(long UserId) : IRequest<List<SavedShelterDto>>;
