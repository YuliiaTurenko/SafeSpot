using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.Exceptions;

namespace SafeSpot.Application.Features.Comments.Commands.Update;

public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand>
{
    private readonly ICommentRepository _commentRepo;

    public UpdateCommentCommandHandler(ICommentRepository commentRepo)
    {
        _commentRepo = commentRepo;
    }

    public async Task Handle(UpdateCommentCommand request, CancellationToken ct)
    {
        var comment = await _commentRepo.GetByIdAsync(request.CommentId);
        
        if (comment == null)
            throw new NotFoundException("Comment not found");
        
        if (comment.UserId != request.UserId)
            throw new ForbiddenException("You can only edit your own comments");
        
        comment.Text = request.Text;
        
        await _commentRepo.UpdateAsync(comment);
    }
}
