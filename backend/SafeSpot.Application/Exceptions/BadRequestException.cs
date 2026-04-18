namespace SafeSpot.Application.Exceptions;

public class BadRequestException : Exception
{
    public Dictionary<string, string[]> ValidationErrors { get; set; } = new();

    public BadRequestException(string message) : base(message) { }

    public BadRequestException(string message, Dictionary<string, string[]> errors)
        : base(message)
    {
        ValidationErrors = errors;
    }
}