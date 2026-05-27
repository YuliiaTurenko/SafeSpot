using MediatR;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Shelters.Queries.GetAnalytics;

public record GetShelterAnalyticsQuery(long ShelterId) : IRequest<ShelterAnalyticsDto>;