namespace TaskMaster.Controllers.Payloads.Responses;

public class Response<T>
{
    public bool Succeeded { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public static Response<T> Succeed(T data)
    {
        return new Response<T> { Succeeded = true, Data = data };
    }

    public static Response<T> Fail(Exception ex)
    {
        return new Response<T> { Succeeded = false, Message = ex.Message };
    }
}