using MediatR;

namespace SafeSpot.Application.Features.Announcements.Commands.Delete;

public record DeleteAnnouncementCommand(long UserId, long Id) : IRequest;