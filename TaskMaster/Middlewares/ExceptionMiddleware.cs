using System.Text.Json;
using TaskMaster.Controllers.Payloads.Responses;

namespace TaskMaster.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        context.Response.StatusCode = ex switch
        {
            KeyNotFoundException => StatusCodes.Status404NotFound,
            ArgumentException => StatusCodes.Status400BadRequest,
            ApplicationException => StatusCodes.Status304NotModified,
            NotImplementedException => StatusCodes.Status501NotImplemented,
            _ => context.Response.StatusCode
        };

        await WriteExceptionMessageAsync(context, ex);
    }

    private async Task WriteExceptionMessageAsync(HttpContext context, Exception ex)
    {
        await context.Response.Body.WriteAsync(
            JsonSerializer.SerializeToUtf8Bytes<ApiResponse<string>>(ApiResponse<string>.Fail(ex),
                new JsonSerializerOptions(JsonSerializerDefaults.Web)));
    }
}