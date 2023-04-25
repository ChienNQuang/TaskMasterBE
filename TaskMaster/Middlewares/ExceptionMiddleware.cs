using System.Collections.ObjectModel;
using System.Text.Json;
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
            await HandleExceptionAsync(context, ex);
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


        // if (ex is NotImplementedException)
        // {
        //     context.Response.StatusCode = StatusCodes.Status501NotImplemented;
        //     await WriteExceptionMessageAsync(context, ex);
        // }

        if (ex is UserValidationException uve)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var data =
                uve.Errors.ToDictionary(vf => vf.PropertyName.ToLower(), vf => vf.ErrorMessage);

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