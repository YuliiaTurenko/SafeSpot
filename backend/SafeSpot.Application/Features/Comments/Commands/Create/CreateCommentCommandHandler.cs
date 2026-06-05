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
    private readonly IUserRoleService _userRoleService;
    private readonly IPostNotificationService _notificationService;

    public CreateCommentCommandHandler(ICommentRepository commentRepo, IPostRepository postRepo, IUserRepository userRepo, IUserRoleService userRoleService, IPostNotificationService notificationService)
    {
        _commentRepo = commentRepo;
        _postRepo = postRepo;
        _userRepo = userRepo;
        _userRoleService = userRoleService;
        _notificationService = notificationService;
    }

    public async Task<long> Handle(CreateCommentCommand request, CancellationToken ct)
    {
        var comment = new Comment
        {
            UserId = request.UserId,
            PostId = request.PostId,
            Text = request.Text
        };

        await _commentRepo.AddAsync(comment);

        var post = await _postRepo.GetByIdAsync(request.PostId);
        if (post != null)
        {
            string? userName = null;
            if (request.UserId.HasValue)
            {
                var user = await _userRepo.GetByIdAsync(request.UserId.Value);
                if (user != null)
                {
                    userName = await _userRoleService.GetEmailByIdentityIdAsync(user.IdentityId);
                }
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

            await _notificationService.NotifyNewCommentAsync(post.ShelterId, commentDto);
        }

        return comment.Id;
    }
}
