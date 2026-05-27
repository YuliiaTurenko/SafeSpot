using MediatR;

namespace SafeSpot.Application.Features.Notifications.Commands.MarkAsRead;

public record MarkNotificationAsReadCommand(long id) : IRequest;