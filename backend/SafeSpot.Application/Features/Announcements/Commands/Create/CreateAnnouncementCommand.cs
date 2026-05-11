using MediatR;

namespace SafeSpot.Application.Features.Announcements.Commands.Create;

public record CreateAnnouncementCommand(
    long UserId,
    long ShelterId,
    string Title,
    string Text,
    string? ImageUrl
) : IRequest<long>;