using MediatR;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.Announcements.Commands.Update;

public class UpdateAnnouncementCommandHandler : IRequestHandler<UpdateAnnouncementCommand>
{
    private readonly IAnnouncementRepository _repo;

    public UpdateAnnouncementCommandHandler(IAnnouncementRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(UpdateAnnouncementCommand request, CancellationToken ct)
    {
        var announcement = await _repo.GetByIdAsync(request.Id);

        announcement.Title = request.Title;
        announcement.Text = request.Text;
        announcement.ImageUrl = request.ImageUrl;

        _repo.Update(announcement);
    }
}