using MediatR;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.User.Queries.GetById;
public record GetUserByIdQuery(long Id) : IRequest<UserDto>;