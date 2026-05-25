using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.DTOs;

public class ShelterPreviewDto
{
    public long Id { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int Capacity { get; set; }
    public ShelterStatus Status { get; set; }
}