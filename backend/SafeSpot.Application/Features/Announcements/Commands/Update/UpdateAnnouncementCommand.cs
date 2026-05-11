using MediatR;

namespace SafeSpot.Application.Features.Announcements.Commands.Update;

public record UpdateAnnouncementCommand(
    long UserId,
    long Id,
    string Title,
    string Text,
    string? ImageUrl
) : IRequest;