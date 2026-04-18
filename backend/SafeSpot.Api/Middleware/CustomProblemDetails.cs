namespace SafeSpot.Api.Middleware;

public class CustomProblemDetails
{
    public string Title { get; set; }
    public int Status { get; set; }
    public string Message { get; set; }
    public object? Errors { get; set; }
}
