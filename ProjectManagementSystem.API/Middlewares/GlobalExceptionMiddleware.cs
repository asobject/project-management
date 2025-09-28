using System.Net;

namespace ProjectManagementSystem.API.Middlewares;

public class GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger) : IMiddleware
{

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
            logger.LogError(ex, "An unexpected error occurred.");

        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = HttpStatusCode.InternalServerError;
        var errorType = "UnknownError";
        var errorMessage = exception.Message;

        context.Response.StatusCode = (int)statusCode;
        var result = new
        {
            message = errorMessage,
            type = errorType
        };

        return context.Response.WriteAsJsonAsync(result);
    }
}