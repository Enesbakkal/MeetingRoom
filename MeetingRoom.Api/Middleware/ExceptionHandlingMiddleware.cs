using System.Net;
using System.Text.Json;
using MeetingRoom.Api.Models;

namespace MeetingRoom.Api.Middleware;

// Döndürülen HTTP durum kodları:
// 400 Bad Request  -> InvalidOperationException, ArgumentException (örn. conflict, validasyon)
// 404 Not Found    -> KeyNotFoundException
// 500 Internal Server Error -> Diğer tüm hatalar
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await HandleAsync(context, ex);
        }
    }

    private static async Task HandleAsync(HttpContext context, Exception ex)
    {
        // 400: InvalidOperationException, ArgumentException | 404: KeyNotFoundException | 500: diğer
        var (statusCode, message) = ex switch
        {
            InvalidOperationException => (HttpStatusCode.BadRequest, ex.Message),   // 400
            ArgumentException => (HttpStatusCode.BadRequest, ex.Message),           // 400
            KeyNotFoundException => (HttpStatusCode.NotFound, ex.Message),            // 404
            _ => (HttpStatusCode.InternalServerError, "An error occurred.")         // 500
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new ApiResponse<object>
        {
            Success = false,
            Message = message,
            Errors = statusCode == HttpStatusCode.InternalServerError ? null : new List<string> { ex.Message }
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
