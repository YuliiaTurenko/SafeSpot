using MediatR;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.User.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, long>
{
    private readonly IUserRepository _repo;
    private readonly IUserContext _userContext;

    public CreateUserCommandHandler(IUserRepository repo, IUserContext userContext)
    {
        _repo = repo;
        _userContext = userContext;
    }

    public async Task<long> Handle(CreateUserCommand request, CancellationToken ct)
    {
        var identityId = _userContext.GetApplicationUserId();

        var user = new Domain.Entities.User
        {
            IdentityId = identityId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(user);

        return user.Id;
    }
}