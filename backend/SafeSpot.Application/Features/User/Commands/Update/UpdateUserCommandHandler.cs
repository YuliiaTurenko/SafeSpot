using MediatR;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.User.Commands.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUserRepository _repo;

    public UpdateUserCommandHandler(IUserRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken ct)
    {
        Domain.Entities.User user = await _repo.GetByIdAsync(request.Id);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        _repo.Update(user);
    }
}