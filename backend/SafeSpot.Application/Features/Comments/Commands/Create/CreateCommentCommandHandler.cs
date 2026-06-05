using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Features.Comments.Commands.Create;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, long>
{
    private readonly ICommentRepository _commentRepo;
    private readonly IUserRepository _userRepo;

    public CreateCommentCommandHandler(ICommentRepository commentRepo, IUserRepository userRepo)
    {
        _commentRepo = commentRepo;
        _userRepo = userRepo;
    }

    public async Task<long> Handle(CreateCommentCommand request, CancellationToken ct)
    {
        long userId = await _userRepo.GetUserIdByIdentityIdAsync(request.IdentityId);

        var comment = new Comment
        {
            UserId = userId,
            PostId = request.PostId,
            Text = request.Text
        };

        await _commentRepo.AddAsync(comment);

        return comment.Id;
    }
}
