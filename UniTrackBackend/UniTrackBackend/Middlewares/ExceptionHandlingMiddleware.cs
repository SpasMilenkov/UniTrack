using System.Net;
using System.Text.Json;
using UniTrackBackend.Services.Commons.Exceptions;

namespace UniTrackBackend.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; // 500 if unexpected
        string result;

        switch (exception)
        {
            case ArgumentNullException e:
                code = HttpStatusCode.BadRequest;
                result = e.Message;
                break;
            case UnauthorizedAccessException e:
                code = HttpStatusCode.Unauthorized;
                result = e.Message;
                break;
            case DataNotFoundException e:
                code = HttpStatusCode.NotFound;
                result = e.Message;
                break;
            case JwtException e:
                code = HttpStatusCode.BadRequest;
                result = e.Message;
                break;
            case ValidationFailedException e:
                code = HttpStatusCode.BadRequest;
                result = e.Message;
                break;
            case OperationFailedException e:
                code = HttpStatusCode.InternalServerError;
                result = e.Message;
                break;
            default:
                code = HttpStatusCode.InternalServerError;
                result = "An unexpected error occurred.";
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(JsonSerializer.Serialize(new { error = result }));
    }
}