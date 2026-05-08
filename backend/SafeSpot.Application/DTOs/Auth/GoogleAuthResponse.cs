namespace SafeSpot.Application.DTOs.Auth;

public class GoogleAuthResponse
{
    public string Token { get; set; }
    public string Email { get; set; }
    public bool RequiresProfileCompletion { get; set; }
}
