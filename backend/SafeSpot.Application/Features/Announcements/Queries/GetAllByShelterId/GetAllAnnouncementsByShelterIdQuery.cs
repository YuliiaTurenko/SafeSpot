using MediatR;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Announcements.Queries.GetAllByShelterId;

public record GetAllAnnouncementsByShelterIdQuery(long ShelterId) : IRequest<List<AnnouncementDto>>;