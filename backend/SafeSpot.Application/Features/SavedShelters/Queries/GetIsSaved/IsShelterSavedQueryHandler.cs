using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSpot.Application.Features.SavedShelters.Queries.GetIsSaved;

public class IsShelterSavedQueryHandler : IRequestHandler<IsShelterSavedQuery, bool>
{
    private readonly ISavedShelterRepository _repository;

    public IsShelterSavedQueryHandler(ISavedShelterRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(IsShelterSavedQuery request, CancellationToken cancellationToken)
    {
        return await _repository
            .IsShelterSavedAsync(
                request.UserId,
                request.ShelterId,
                SavedShelterType.Favorite);
    }
}