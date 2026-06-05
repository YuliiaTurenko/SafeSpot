using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.DTOs;

public class SavedShelterDto
{
    public long ShelterId { get; set; }
    public string Address { get; set; }
    public int Capacity { get; set; }
    public ShelterStatus Status { get; set; }
    public string? ImageUrl { get; set; }
}
