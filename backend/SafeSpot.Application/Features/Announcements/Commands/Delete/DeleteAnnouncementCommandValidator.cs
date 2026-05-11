using FluentValidation;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.Announcements.Commands.Delete;

public class DeleteAnnouncementCommandValidator : AbstractValidator<DeleteAnnouncementCommand>
{
    private readonly IAnnouncementRepository _repo;

    public DeleteAnnouncementCommandValidator(IAnnouncementRepository repo)
    {
        _repo = repo;

        RuleFor(x => x)
            .MustAsync(AnnouncementExists).WithMessage("Announcement not found.")
            .MustAsync(UserHasPermission).WithMessage("You can't delete other's announcement.");
    }

    private async Task<bool> AnnouncementExists(DeleteAnnouncementCommand cmd, CancellationToken ct)
    {
        return await _repo.ExistsByIdAsync(cmd.Id);
    }

    private async Task<bool> UserHasPermission(DeleteAnnouncementCommand cmd, CancellationToken ct)
    {
        return await _repo.UserOwnsAnnouncementAsync(cmd.UserId, cmd.Id);
    }
}
