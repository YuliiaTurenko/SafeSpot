using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.SavedShelters.Queries.GetFavorite;

public class GetFavoriteSheltersQueryHandler : IRequestHandler<GetFavoriteSheltersQuery, List<SavedShelterDto>>
{
    private readonly ISavedShelterRepository _repository;

    public GetFavoriteSheltersQueryHandler(ISavedShelterRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<SavedShelterDto>> Handle(GetFavoriteSheltersQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetFavoriteSheltersAsync(request.UserId);
    }
}