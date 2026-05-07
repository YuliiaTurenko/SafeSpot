using MediatR;

namespace SafeSpot.Application.Features.User.Commands.Create;

public record CreateUserCommand(
    string FirstName,
    string LastName
) : IRequest<long>;