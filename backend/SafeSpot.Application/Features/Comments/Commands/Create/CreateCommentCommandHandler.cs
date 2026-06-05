using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Features.Comments.Commands.Create;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, long>
{
    private readonly ICommentRepository _commentRepo;
    private readonly IPostRepository _postRepo;
    private readonly IUserRepository _userRepo;

    public CreateCommentCommandHandler(ICommentRepository commentRepo, IPostRepository postRepo, IUserRepository userRepo)
    {
        _commentRepo = commentRepo;
        _postRepo = postRepo;
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

        var post = await _postRepo.GetByIdAsync(request.PostId);
        if (post != null)
        {
            string? userName = null;

            if (request.IdentityId != null)
            {
                string firstName = await _userRepo.GetUserFirstNameByIdentityIdAsync(request.IdentityId);
                string lastName = await _userRepo.GetUserLastNameByIdentityIdAsync(request.IdentityId);

                userName = $"{firstName} {lastName}".Trim();
            }

            var commentDto = new CommentDto
            {
                Id = comment.Id,
                UserId = comment.UserId,
                UserName = userName,
                PostId = comment.PostId,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt
            };
        }

        return comment.Id;
    }
}
