using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Domain.Exceptions;

namespace SortingApi.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception e)
        {
            _logger.LogError(e, $"Exception occurred: {e.Message}");

            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = GetHtttpStatusCode(exception);

        var response = GetSerializedErrorDetails(exception);

        await httpContext.Response.WriteAsync(response);

    }

    private static int GetHtttpStatusCode(Exception exception) => exception switch
    {
        ValidationException => StatusCodes.Status400BadRequest,
        ArrayNotFoundException => StatusCodes.Status404NotFound,
        _ => StatusCodes.Status500InternalServerError
    };

    private static string GetSerializedErrorDetails(Exception exception)
    {
        var response = new
        {
            Message = exception.Message,
            StackTrace = exception.StackTrace
        };

        return JsonSerializer.Serialize(response);
    }
}
