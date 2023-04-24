using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
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

        if (ex is KeyNotFoundException)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }

        if (ex is ArgumentException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }

        if (ex is ApplicationException)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }

        if (ex is NotImplementedException)
        {
            context.Response.StatusCode = StatusCodes.Status501NotImplemented;
        }

        await WriteExceptionAsync(context, ex);
    }

    private async Task WriteExceptionAsync(HttpContext context, Exception ex)
    {
        await context.Response.Body.WriteAsync(
            JsonSerializer.SerializeToUtf8Bytes<ApiResponse<string>>(ApiResponse<string>.Fail(ex),
                new JsonSerializerOptions(JsonSerializerDefaults.Web)));
    }
}