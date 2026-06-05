using MediatR;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Posts.Queries.GetByShelterId;

public record GetPostsByShelterIdQuery(long ShelterId, string IdentityId) : IRequest<List<PostDto>>;
