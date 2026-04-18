using SafeSpot.Application.Abstractions;
using SafeSpot.Application.Exceptions;
using System.Net;
using System.Text.Json;
using UnauthorizedAccessException = SafeSpot.Application.Exceptions.UnauthorizedAccessException;

namespace SafeSpot.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly ILocalizationService _loc;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger,
        ILocalizationService loc)
    {
        _next = next;
        _logger = logger;
        _loc = loc;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var lang = context.Request.Headers["Accept-Language"].ToString() ?? "en";

        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

        CustomProblemDetails problem = new CustomProblemDetails
        {
            Title = "Error",
            Status = (int)statusCode,
            Message = _loc.Get("ServerError", lang)
        };

        switch (ex)
        {
            case BadRequestException badRequest:
                statusCode = HttpStatusCode.BadRequest;

                problem = new CustomProblemDetails
                {
                    Title = "Bad Request",
                    Status = (int)statusCode,
                    Message = badRequest.Message,
                    Errors = badRequest.ValidationErrors
                };

                _logger.LogWarning(ex, badRequest.Message);
                break;

            case NotFoundException notFound:
                statusCode = HttpStatusCode.NotFound;

                problem = new CustomProblemDetails
                {
                    Title = "Not Found",
                    Status = (int)statusCode,
                    Message = _loc.Get("NotFound", lang)
                };

                _logger.LogWarning(ex, notFound.Message);
                break;

            case ForbiddenException forbidden:
                statusCode = HttpStatusCode.Forbidden;

                problem = new CustomProblemDetails
                {
                    Title = "Forbidden",
                    Status = (int)statusCode,
                    Message = _loc.Get("Forbidden", lang)
                };

                _logger.LogWarning(ex, forbidden.Message);
                break;

            case UnauthorizedAccessException unauthorized:
                statusCode = HttpStatusCode.Unauthorized;

                problem = new CustomProblemDetails
                {
                    Title = "Unauthorized",
                    Status = (int)statusCode,
                    Message = _loc.Get("Unauthorized", lang)
                };

                _logger.LogWarning(ex, "Unauthorized");
                break;

            case Exception:
                statusCode = HttpStatusCode.InternalServerError;

                problem = new CustomProblemDetails
                {
                    Title = "Server Error",
                    Status = (int)statusCode,
                    Message = _loc.Get("ServerError", lang)
                };

                _logger.LogError(ex, "Server error");
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(problem);

        await context.Response.WriteAsync(json);
    }
}