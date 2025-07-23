using Microsoft.EntityFrameworkCore;
using TaskifyAPI.Exceptions;

namespace TaskifyAPI.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message, errorCode) = exception switch
        {
            ArgumentException => (StatusCodes.Status400BadRequest, "Internal error", "09X0"),
            EmailAlreadyExistException => (StatusCodes.Status409Conflict, "Email already exists", "09X23"),
            UserNotFoundException => (StatusCodes.Status404NotFound, "User not found", "09X13"),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Internal error ", "09X3"),
            DbUpdateException => (StatusCodes.Status409Conflict, "Internal error - ", "09X4"),
            _ => (StatusCodes.Status500InternalServerError, "Internal error - ", "09X2")
        };
        
        context.Response.StatusCode = statusCode;
        
        var problemDetails = new
        {
            StatusCode = statusCode,
            Message = message,
            ErrorCode = errorCode,
            Details = _env.IsDevelopment() ? exception.ToString() : null,
            StackTrace = _env.IsDevelopment() ? exception.StackTrace : null,
        };
        
        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}