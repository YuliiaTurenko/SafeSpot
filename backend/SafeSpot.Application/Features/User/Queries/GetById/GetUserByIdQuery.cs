using MediatR;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.User.Queries.GetById;
public record GetUserByIdQuery(string Id) : IRequest<UserDto>;