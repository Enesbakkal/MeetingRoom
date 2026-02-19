using System.Net;
using System.Text.Json;
using FluentValidation;
using MeetingRoom.Api.Models;

namespace MeetingRoom.Api.Middleware;

// Döndürülen HTTP durum kodları:
// 400 Bad Request  -> ValidationException (FluentValidation), InvalidOperationException, ArgumentException
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
        // 400: ValidationException, InvalidOperationException, ArgumentException | 404: KeyNotFoundException | 500: diğer
        var (statusCode, message, errors) = ex switch
        {
            ValidationException validation => (HttpStatusCode.BadRequest, "Validation failed.", validation.Errors.Select(e => e.ErrorMessage).ToList()),
            InvalidOperationException => (HttpStatusCode.BadRequest, ex.Message, new List<string> { ex.Message }),
            ArgumentException => (HttpStatusCode.BadRequest, ex.Message, new List<string> { ex.Message }),
            KeyNotFoundException => (HttpStatusCode.NotFound, ex.Message, new List<string> { ex.Message }),
            _ => (HttpStatusCode.InternalServerError, "An error occurred.", (List<string>?)null)
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new ApiResponse<object>
        {
            Success = false,
            Message = message,
            Errors = errors
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
