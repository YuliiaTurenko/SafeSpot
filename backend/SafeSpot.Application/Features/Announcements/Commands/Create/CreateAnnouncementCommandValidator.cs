using FluentValidation;


namespace SafeSpot.Application.Features.Announcements.Commands.Create;

public class CreateAnnouncementCommandValidator : AbstractValidator<CreateAnnouncementCommand>
{
    public CreateAnnouncementCommandValidator()
    {
        RuleFor(x => x.Title).MaximumLength(100).NotEmpty();
        RuleFor(x => x.Text).MaximumLength(600).NotEmpty();
    }
}
