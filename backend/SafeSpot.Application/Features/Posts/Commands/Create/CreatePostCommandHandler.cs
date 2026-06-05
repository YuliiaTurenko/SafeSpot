using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Features.Posts.Commands.Create;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, long>
{
    private readonly IPostRepository _postRepo;
    private readonly IUserRepository _userRepo;
    private readonly IPostNotificationService _notificationService;

    public CreatePostCommandHandler(IPostRepository postRepo, IUserRepository userRepo, IPostNotificationService notificationService)
    {
        _postRepo = postRepo;
        _userRepo = userRepo;
        _notificationService = notificationService;
    }

    public async Task<long> Handle(CreatePostCommand request, CancellationToken ct)
    {
        long userId = await _userRepo.GetUserIdByIdentityIdAsync(request.IdentityId);

        var post = new Post
        {
            UserId = userId,
            ShelterId = request.ShelterId,
            Text = request.Text
        };

        await _postRepo.AddAsync(post);

        string? userName = null;

        if (request.IdentityId != null)
        {
            string firstName = await _userRepo.GetUserFirstNameByIdentityIdAsync(request.IdentityId);
            string lastName = await _userRepo.GetUserLastNameByIdentityIdAsync(request.IdentityId);

            userName = $"{firstName} {lastName}".Trim();
        }

        var postDto = new PostDto
        {
            Id = post.Id,
            UserId = post.UserId,
            UserName = userName,
            ShelterId = post.ShelterId,
            Text = post.Text,
            CreatedAt = post.CreatedAt
        };

        await _notificationService.NotifyNewPostAsync(request.ShelterId, postDto);

        return post.Id;
    }
}
