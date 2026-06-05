using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.Exceptions;

namespace SafeSpot.Application.Features.Posts.Commands.Delete;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
{
    private readonly IPostRepository _postRepo;

    public DeletePostCommandHandler(IPostRepository postRepo)
    {
        _postRepo = postRepo;
    }

    public async Task Handle(DeletePostCommand request, CancellationToken ct)
    {
        var post = await _postRepo.GetByIdAsync(request.PostId);
        
        if (post == null)
            throw new NotFoundException("Post not found");
        
        if (post.UserId != request.UserId)
            throw new ForbiddenException("You can only delete your own posts");
        
        await _postRepo.DeleteAsync(post);
    }
}
