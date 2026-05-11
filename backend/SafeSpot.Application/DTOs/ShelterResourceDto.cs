using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.DTOs;

public class ShelterResourceDto
{
    public long Id { get; set; }
    public ResourceType Type { get; set; }
    public ResourceStatus Status { get; set; }
    public int Amount { get; set; }
}
