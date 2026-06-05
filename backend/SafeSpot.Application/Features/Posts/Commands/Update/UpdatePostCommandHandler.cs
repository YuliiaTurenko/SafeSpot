using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.Exceptions;

namespace SafeSpot.Application.Features.Posts.Commands.Update;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand>
{
    private readonly IPostRepository _postRepo;

    public UpdatePostCommandHandler(IPostRepository postRepo)
    {
        _postRepo = postRepo;
    }

    public async Task Handle(UpdatePostCommand request, CancellationToken ct)
    {
        var post = await _postRepo.GetByIdAsync(request.PostId);
        
        if (post == null)
            throw new NotFoundException("Post not found");
        
        if (post.UserId != request.UserId)
            throw new ForbiddenException("You can only edit your own posts");
        
        post.Text = request.Text;
        
        await _postRepo.UpdateAsync(post);
    }
}
