namespace SafeSpot.Api.Contracts.Announcements;

public class CreateAnnouncementRequest
{
    public long ShelterId { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public string? ImageUrl { get; set; }
}
