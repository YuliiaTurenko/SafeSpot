using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.Exceptions;

namespace SafeSpot.Application.Features.Comments.Commands.Delete;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
{
    private readonly ICommentRepository _commentRepo;

    public DeleteCommentCommandHandler(ICommentRepository commentRepo)
    {
        _commentRepo = commentRepo;
    }

    public async Task Handle(DeleteCommentCommand request, CancellationToken ct)
    {
        var comment = await _commentRepo.GetByIdAsync(request.CommentId);
        
        if (comment == null)
            throw new NotFoundException("Comment not found");
        
        if (comment.UserId != request.UserId)
            throw new ForbiddenException("You can only delete your own comments");
        
        await _commentRepo.DeleteAsync(comment);
    }
}
