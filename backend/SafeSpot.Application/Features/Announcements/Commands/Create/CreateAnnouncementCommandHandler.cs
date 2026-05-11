using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Features.Announcements.Commands.Create;

public class CreateAnnouncementCommandHandler : IRequestHandler<CreateAnnouncementCommand, long>
{
    private readonly IAnnouncementRepository _repo;

    public CreateAnnouncementCommandHandler(IAnnouncementRepository repo)
    {
        _repo = repo;
    }

    public async Task<long> Handle(CreateAnnouncementCommand request, CancellationToken ct)
    {
        var announcement = new Announcement
        {
            UserId = request.UserId,
            ShelterId = request.ShelterId,
            Title = request.Title,
            Text = request.Text,
            ImageUrl = request.ImageUrl,
        };

        await _repo.AddAsync(announcement);

        return announcement.Id;
    }
}