namespace TaskMaster.Controllers.Payloads.Requests;

public class UserUpdateRequest
{
    public Guid Id { get; set; }
    public string Password { get; set; }
}