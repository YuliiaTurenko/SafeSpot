using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Announcements.Queries.GetAllByShelterId;

public class GetAllAnnouncementsByShelterIdQueryHandler : IRequestHandler<GetAllAnnouncementsByShelterIdQuery, List<AnnouncementDto>>
{
    private readonly IAnnouncementRepository _repo;

    public GetAllAnnouncementsByShelterIdQueryHandler(IAnnouncementRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<AnnouncementDto>> Handle(GetAllAnnouncementsByShelterIdQuery request, CancellationToken ct)
    {
        var announcements = await _repo.GetAllByShelterIdAsync(request.ShelterId);

        return announcements.Select(x => new AnnouncementDto
        {
            Id = x.Id,
            Title = x.Title,
            Text = x.Text,
            ImageUrl = x.ImageUrl,
        }).ToList();
    }
}