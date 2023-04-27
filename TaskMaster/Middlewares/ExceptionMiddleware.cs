using System.Collections.ObjectModel;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TaskMaster.Controllers.Payloads.Responses;
using TaskMaster.Exceptions;

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
            try
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception e)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        if (ex is KeyNotFoundException)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await WriteExceptionMessageAsync(context, ex);
        }


        if (ex is ArgumentException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await WriteExceptionMessageAsync(context, ex);
        }


        if (ex is ApplicationException)
        {
            context.Response.StatusCode = StatusCodes.Status304NotModified;       
            await WriteExceptionMessageAsync(context, ex);
        }

        if (ex is UserDuplicateException)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;       
            await WriteExceptionMessageAsync(context, ex);
        }

        if (ex is DbUpdateException)
        {
            context.Response.StatusCode = StatusCodes.Status304NotModified;
            await WriteExceptionMessageAsync(context, ex);
        }

        if (ex is RequestValidationException uve)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var data =
                uve.Errors!.ToDictionary(vf => vf.PropertyName.ToLower(), vf => vf.ErrorMessage);

            var apiResponse = ApiResponse<Dictionary<string, string>>.Fail(ex) with
            {
                Data = data
            };
            await context.Response.Body.WriteAsync(SerializeToUtf8BytesWeb(apiResponse));
        }
        
    }

    private static async Task WriteExceptionMessageAsync(HttpContext context, Exception ex)
    {
        await context.Response.Body.WriteAsync(SerializeToUtf8BytesWeb(ApiResponse<string>.Fail(ex)));
    }

    private static byte[] SerializeToUtf8BytesWeb<T>(T value)
    {
        return JsonSerializer.SerializeToUtf8Bytes<T>(value, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
}