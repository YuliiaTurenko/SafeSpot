using MediatR;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.Announcements.Commands.Delete;

public class DeleteAnnouncementCommandHandler : IRequestHandler<DeleteAnnouncementCommand>
{
    private readonly IAnnouncementRepository _repo;

    public DeleteAnnouncementCommandHandler(IAnnouncementRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(DeleteAnnouncementCommand request, CancellationToken ct)
    {
        var announcement = await _repo.GetByIdAsync(request.Id);

        _repo.Delete(announcement);
    }
}