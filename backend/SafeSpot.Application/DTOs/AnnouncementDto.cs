namespace SafeSpot.Application.DTOs;

public class AnnouncementDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public string? ImageUrl { get; set; }
}
