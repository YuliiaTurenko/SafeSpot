namespace SafeSpot.Application.DTOs;

public class PostDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string? UserName { get; set; }
    public long ShelterId { get; set; }
    public string Text { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
