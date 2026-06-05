using MediatR;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Comments.Queries.GetByPostId;

public record GetCommentsByPostIdQuery(long PostId, string IdentityId) : IRequest<List<CommentDto>>;
