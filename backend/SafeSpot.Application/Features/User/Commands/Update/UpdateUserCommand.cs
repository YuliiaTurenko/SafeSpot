using MediatR;

namespace SafeSpot.Application.Features.User.Commands.Update;

public record UpdateUserCommand(
    long Id,
    string? FirstName,
    string? LastName
) : IRequest;