using System.Net;
using System.Text.Json;
using DataInCloud.Orchestrators.Exception;

namespace DataOnCloud.Api;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (System.Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(
        HttpContext context,
        System.Exception exception)
    {
        string exceptionResult = exception.Message;
        context.Response.ContentType = "application/json";

        switch (exception)
        {
            case ResourceNotFoundException e:
                {
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    exceptionResult = JsonSerializer.Serialize(new { error = e.Message });
                    break;
                }
            default:
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    exceptionResult = JsonSerializer.Serialize(new { error = exception.Message });
                    break;
                }
        }


        return context.Response.WriteAsync(exceptionResult);
    }
}