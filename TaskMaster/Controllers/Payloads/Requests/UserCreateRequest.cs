namespace TaskMaster.Controllers.Payloads.Requests;

public class UserCreateRequest
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}