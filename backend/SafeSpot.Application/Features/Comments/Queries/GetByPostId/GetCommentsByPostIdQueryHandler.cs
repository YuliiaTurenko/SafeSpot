using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Comments.Queries.GetByPostId;

public class GetCommentsByPostIdQueryHandler : IRequestHandler<GetCommentsByPostIdQuery, List<CommentDto>>
{
    private readonly ICommentRepository _commentRepo;
    private readonly IUserRepository _userRepo;

    public GetCommentsByPostIdQueryHandler(ICommentRepository commentRepo, IUserRepository userRepo)
    {
        _commentRepo = commentRepo;
        _userRepo = userRepo;
    }

    public async Task<List<CommentDto>> Handle(GetCommentsByPostIdQuery request, CancellationToken ct)
    {
        var comments = await _commentRepo.GetByPostIdAsync(request.PostId);

        var result = new List<CommentDto>();

        foreach (var comment in comments)
        {
            string? userName = null;
            if (comment.User != null)
            {
                string firstName = await _userRepo.GetUserFirstNameByIdentityIdAsync(request.IdentityId);
                string lastName = await _userRepo.GetUserLastNameByIdentityIdAsync(request.IdentityId);

                userName = $"{firstName} {lastName}".Trim();
            }

            result.Add(new CommentDto
            {
                Id = comment.Id,
                UserId = comment.UserId,
                UserName = userName,
                PostId = comment.PostId,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt
            });
        }

        return result;
    }
}
