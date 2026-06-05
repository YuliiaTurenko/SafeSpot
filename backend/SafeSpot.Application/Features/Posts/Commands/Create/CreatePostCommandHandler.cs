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
        var post = new Post
        {
            UserId = request.UserId,
            ShelterId = request.ShelterId,
            Text = request.Text
        };

        await _postRepo.AddAsync(post);

        string? userName = null;
        string firstName = await _userRepo.GetUserFirstNameByUserIdAsync(request.UserId);
        string lastName = await _userRepo.GetUserLastNameByUserIdAsync(request.UserId);

        userName = $"{firstName} {lastName}".Trim();

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
