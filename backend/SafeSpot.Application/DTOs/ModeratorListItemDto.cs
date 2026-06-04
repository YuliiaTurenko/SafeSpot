namespace SafeSpot.Application.DTOs;

public class ModeratorListItemDto
{
    public long Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<long> ShelterIds { get; set; } = new();
}
