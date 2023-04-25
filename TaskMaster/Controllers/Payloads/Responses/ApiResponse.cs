namespace TaskMaster.Controllers.Payloads.Responses;

public record ApiResponse<T>
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public static ApiResponse<T> Succeed(T data)
    {
        return new ApiResponse<T> { Succeeded = true, Data = data };
    }

    public static ApiResponse<T> Fail(Exception ex)
    {
        return new ApiResponse<T> { Succeeded = false, Message = ex.Message };
    }
}