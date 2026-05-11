namespace SafeSpot.Api.Contracts.Announcements;

public class UpdateAnnouncementRequest
{
    public long AnnouncementId { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public string? ImageUrl { get; set; }
}
