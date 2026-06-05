using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Posts.Queries.GetByShelterId;

public class GetPostsByShelterIdQueryHandler : IRequestHandler<GetPostsByShelterIdQuery, List<PostDto>>
{
    private readonly IPostRepository _postRepo;
    private readonly IUserRepository _userRepo;

    public GetPostsByShelterIdQueryHandler(IPostRepository postRepo, IUserRepository userRepo)
    {
        _postRepo = postRepo;
        _userRepo = userRepo;
    }

    public async Task<List<PostDto>> Handle(GetPostsByShelterIdQuery request, CancellationToken ct)
    {
        var posts = await _postRepo.GetByShelterIdAsync(request.ShelterId);

        var result = new List<PostDto>();

        foreach (var post in posts)
        {
            string? userName = null;
            if (post.User != null)
            {
                string firstName = await _userRepo.GetUserFirstNameByIdentityIdAsync(request.IdentityId);
                string lastName = await _userRepo.GetUserLastNameByIdentityIdAsync(request.IdentityId);

                userName = $"{firstName} {lastName}".Trim();
            }

            result.Add(new PostDto
            {
                Id = post.Id,
                UserId = post.UserId,
                UserName = userName,
                ShelterId = post.ShelterId,
                Text = post.Text,
                CreatedAt = post.CreatedAt
            });
        }

        return result;
    }
}
