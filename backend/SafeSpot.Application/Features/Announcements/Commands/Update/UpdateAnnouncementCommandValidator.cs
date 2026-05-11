using FluentValidation;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.Announcements.Commands.Update;

public class UpdateAnnouncementCommandValidator : AbstractValidator<UpdateAnnouncementCommand>
{
    private readonly IAnnouncementRepository _repo;

    public UpdateAnnouncementCommandValidator(IAnnouncementRepository repo)
    {
        _repo = repo;

        RuleFor(x => x.Title).MaximumLength(100).NotEmpty();
        RuleFor(x => x.Text).MaximumLength(600).NotEmpty();

        RuleFor(x => x)
           .MustAsync(AnnouncementExists).WithMessage("Announcement not found.")
           .MustAsync(UserHasPermission).WithMessage("You can't edit other's announcement.");
    }

    private async Task<bool> AnnouncementExists(UpdateAnnouncementCommand cmd, CancellationToken ct)
    {
        return await _repo.ExistsByIdAsync(cmd.Id);
    }

    private async Task<bool> UserHasPermission(UpdateAnnouncementCommand cmd, CancellationToken ct)
    {
        return await _repo.UserOwnsAnnouncementAsync(cmd.UserId, cmd.Id);
    }
}
